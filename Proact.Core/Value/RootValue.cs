using Proact.Core.Tag.Context;

namespace Proact.Core.Value;

public class RootValue<T> : ValueBase<T>
{
    private readonly RootValueOptions<T> _options;
    public Javascript<T> Js { get; }

    public RootValue(string id, T initialValue) : this(id, new RootValueOptions<T> {InitialValue = initialValue})
    {
    }

    public RootValue(string id, RootValueOptions<T> options)
    {
        Id = id;
        RootId = id;
        _options = options;
        Js = new Javascript<T>(id);
    }
    
    public void OnChange(Action<T, IRenderContext> onChange)
    {
        SideEffects.Add(SideEffect.Create(onChange));
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
        value = Js.GetFromValueSetter(value, renderContext);
        renderContext.CalculatedValues[Id] = value;
        return value;
    }

    private object GetValueWithoutSetter(IRenderContext renderContext)
    {
        var valueChangeOptions = renderContext.ValueChanges.GetValueOrDefault(Id);
        if (valueChangeOptions != null)
        {
            return Json.Parse<T>(valueChangeOptions.Value);
        }

        if (_options.InitialValueCreator != null)
        {
            return _options.InitialValueCreator(renderContext);
        }
        return _options.InitialValue;
    }
}