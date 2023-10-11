using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core.Value;

public class Javascript<T>
{
    private readonly string _id;

    private readonly Dictionary<string, Func<T, IRenderContext, T>> _valueSetters = new();

    public Javascript(string id)
    {
        _id = id;
    }

    public JavascriptCode Run()
    {
        return new JavascriptCode($"changeDynamicValue('{_id}')");
    }
    
    public JavascriptCode SetFromThisValue()
    {
        return new JavascriptCode($"changeDynamicValue('{_id}', this.value)");
    }

    public JavascriptCode SetOnSubmit()
    {
        return new JavascriptCode($"proactFormSubmit('{_id}', event)");
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
    
    internal object? GetFromValueSetter(object value, RenderContext renderContext)
    {
        var valueChangeOptions = renderContext.ValueChanges.GetValueOrDefault(_id);
        if (valueChangeOptions is not { ValueMapperId: not null })
        {
            return value;
        }
        var valueSetter = _valueSetters.GetValueOrDefault(valueChangeOptions.ValueMapperId);
        return valueSetter == null ? value : valueSetter((T) value, renderContext);
    }
    
    
    private JavascriptCode AddSetter(Func<T, IRenderContext, T> setter, string id)
    {
        _valueSetters.Add(id, setter);
        return new JavascriptCode($"changeDynamicValue('{_id}', undefined, {{ValueMapperId: '{id}'}})");
    }
}