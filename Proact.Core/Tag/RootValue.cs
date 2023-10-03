using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class RootValue<T> : HtmlNode, IMappedValue
{
    public string RootId { get; set; }
    public List<IMappedValue> Children { get; set; } = new();

    private readonly Dictionary<string, Func<T, IRenderContext, T>> _valueSetters = new();
    private readonly List<Action<T, IRenderContext>> _sideEffects = new();

    public RootValue(string id, T initialValue)
    {
        Id = id;
        RootId = id;
        InitialValue = initialValue;
    }
    
    public RootValue(string id, Func<IRenderContext, T> initialValueCreator)
    {
        Id = id;
        RootId = id;
        InitialValueCreator = initialValueCreator;
    }

    public string Id { get; set; }
    public T InitialValue { get; set; }
    public Func<IRenderContext, T>? InitialValueCreator { get; set; }
    public IMappedValue? Parent { get; set; } = null;
    
    public override RenderState Render(RenderState renderState)
    {
        var value = GetValue(renderState.RenderContext);
        return RenderValue(renderState, value);
    }
    
    private RenderState RenderValue(RenderState renderState, object renderValue)
    {
        var htmlNode = Json.MapToTag(renderValue);
        htmlNode.Put(Constants.AttributeDynamicValueId, Id);
        htmlNode.Render(renderState);
        renderState.AddDynamicHtmlTags(this);
        if (renderState.RenderContext.ValueChanges.ContainsKey(Id))
        {
            _sideEffects.ForEach(se => se((T)renderValue, renderState.RenderContext));
        }
        return renderState;
    }
    
    public JavascriptCode Run()
    {
        return new JavascriptCode($"changeDynamicValue('{Id}')");
    }
    
    public JavascriptCode SetFromThisValue()
    {
        return new JavascriptCode($"changeDynamicValue('{Id}', this.value)");
    }

    public JavascriptCode SetOnSubmit()
    {
        return new JavascriptCode($"proactFormSubmit('{Id}', event)");
    }

    public JavascriptCode Set(Func<T, IRenderContext, T> setter)
    {
        return AddSetter(setter, IdUtils.CreateId(setter.Method));
    }
    
    public JavascriptCode Set(Func<T, T> setter)
    {
        return AddSetter((v, _) => setter(v), IdUtils.CreateId(setter.Method));
    }
    
    public JavascriptCode Set(Func<T> setter)
    {
        return AddSetter((_, _) => setter(), IdUtils.CreateId(setter.Method));
    }
    
    private JavascriptCode AddSetter(Func<T, IRenderContext, T> setter, string id)
    {
        _valueSetters.Add(id, setter);
        return new JavascriptCode($"changeDynamicValue('{Id}', undefined, {{ValueMapperId: '{id}'}})");
    }

    public void ExecuteSideEffects(RenderContext context)
    {
        if (!context.ValueChanges.ContainsKey(Id))
        {
            return;
        }

        var value = GetValue(context);
        foreach (var sideEffect in _sideEffects)
        {
            sideEffect((T)value, context);
        }
    }

    public object MapValue(RenderContext renderContext, object parentValue)
    {
        // The RootValue has no mapping method
        return parentValue;
    }

    public object GetValue(RenderContext renderContext)
    {
        var value = GetValueWithoutSetter(renderContext);
        value = SetValue(value, renderContext);
        return value;
    }

    private object SetValue(object value, RenderContext renderContext)
    {
        var valueChangeOptions = renderContext.ValueChanges.GetValueOrDefault(Id);
        if (valueChangeOptions is not { ValueMapperId: not null })
        {
            return value;
        }
        var valueSetter = _valueSetters.GetValueOrDefault(valueChangeOptions.ValueMapperId);
        return valueSetter == null ? value : valueSetter((T) value, renderContext);
    }

    public void OnChange(Action<T, IRenderContext> onChange)
    {
        _sideEffects.Add(onChange);
    }

    public object GetValueWithoutSetter(RenderContext renderContext)
    {
        var valueChangeOptions = renderContext.ValueChanges.GetValueOrDefault(Id);
        if (valueChangeOptions != null)
        {
            return Json.Parse<T>(valueChangeOptions.Value);
        }

        if (InitialValueCreator != null)
        {
            return InitialValueCreator(renderContext);
        }
        return InitialValue;
    }

    public MappedValue<T, TMappedValue> Map<TMappedValue>(Func<T, IRenderContext, TMappedValue> valueMapper)
    {
        return MapWithId(valueMapper, IdUtils.CreateId(valueMapper.Method));
    }
    
    public MappedValue<T, TMappedValue> Map<TMappedValue>(Func<T, TMappedValue> valueMapper)
    {
        return MapWithId((t, _) => valueMapper(t), IdUtils.CreateId(valueMapper.Method));
    }
    
    public MappedValue<T, TMappedValue> Map<TMappedValue>(Func<TMappedValue> valueMapper)
    {
        return MapWithId((_, _) => valueMapper(), IdUtils.CreateId(valueMapper.Method));
    }

    private MappedValue<T, TMappedValue> MapWithId<TMappedValue>(Func<T, IRenderContext, TMappedValue> valueMapper, string id)
    {
        var mappedValue = new MappedValue<T, TMappedValue>(valueMapper, this, id);
        Children.Add(mappedValue);
        return mappedValue;
    }
}