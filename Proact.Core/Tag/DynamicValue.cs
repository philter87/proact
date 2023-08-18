namespace Proact.Core.Tag;

public delegate HtmlTag ValueRender<in T> (T? value = default, IServiceProvider? serviceProvider = null);

public delegate T ValueMapper<T>(T value, IServiceProvider? serviceProvider = null);

public class DynamicValue
{
    internal string Id { get; }
    internal object? InitialValue { get; }
    internal ValueMapper<object> ValueMapper = (value, provider) => value;
    internal ValueRender<object>? ValueRender;

    public DynamicValue(string id)
    {
        Id = id;
        InitialValue = default;
    }
    
    public DynamicValue(string id, object initialValue)
    {
        Id = id;
        InitialValue = initialValue;
    }

    public JavascriptCode Run()
    {
        return new JavascriptCode($"trigger('{Id}')");
    }

    public JavascriptCode Run(ValueMapper<object> valueMapper)
    {
        ValueMapper = valueMapper;
        return new JavascriptCode($"trigger('{Id}', {{'IsValueMapper': true, InitialValue: '{InitialValue}'}})");
    }
    
    public JavascriptCode RunUseThisValue()
    {
        return new JavascriptCode($"trigger('{Id}', {{'value': this.value}})");
    }

    public DynamicHtml On(ValueRender<object> valueRender)
    {
        ValueRender = valueRender;
        return new DynamicHtml(this);
    }
}