using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class RootValue<T> : ValueBase<T>
{
    private readonly Dictionary<string, Func<T, IRenderContext, T>> _valueSetters = new();
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
    private T InitialValue { get; set; }
    private Func<IRenderContext, T>? InitialValueCreator { get; set; }
    
    public void OnChange(Action<T, IRenderContext> onChange)
    {
        SideEffects.Add(SideEffect.Create(onChange));
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

    public override object MapValue(RenderContext renderContext, object parentValue)
    {
        // The RootValue has no mapping method
        return parentValue;
    }

    public override object GetValue(RenderContext renderContext)
    {
        if (renderContext.CalculatedValues.TryGetValue(Id, out var cachedValue))
        {
            return cachedValue;
        }
        var value = GetValueWithoutSetter(renderContext);
        value = SetValue(value, renderContext);
        renderContext.CalculatedValues[Id] = value;
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
}