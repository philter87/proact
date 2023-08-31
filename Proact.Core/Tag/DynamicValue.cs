namespace Proact.Core.Tag;
public delegate ValueRender<string> ValueRenderConverter<out T>(ValueRender<T> valueRender);
public delegate ValueMapper<string> ValueMapperConverter<T>(ValueMapper<T> valueMapper);
public delegate HtmlTag ValueRender<in T> (T? value = default, IServiceProvider? serviceProvider = null);

public delegate T ValueMapper<T>(T value, IServiceProvider? serviceProvider = null);

public static class DynamicValue
{
    public static IDynamicValue<int> Create(string triggerId, int initialValue)
    {
        ValueMapperConverter<int> mapperConverter = vm => (v, sp) => vm(int.Parse(v), sp).ToString();
        ValueRenderConverter<int> renderConverter = vr => (v, sp) => vr(int.Parse(v ?? "0"), sp);
        return new DynamicValue<int>(triggerId, initialValue, mapperConverter, renderConverter);
    }
    
    public static IDynamicValue<string> Create(string triggerId, string initialValue)
    {
        return new DynamicValue<string>(triggerId, initialValue, vm => vm, vr => vr);
    }
}

public class DynamicValue<T> : IDynamicValue<T>
{
    private readonly ValueRenderConverter<T> _valueRenderConverter;
    private readonly ValueMapperConverter<T> _valueMapperConverter;
    private readonly DynamicValueObject _state;
    
    public DynamicValue(string id, T initialValue, ValueMapperConverter<T> valueMapperConverter, ValueRenderConverter<T> valueRenderConverter)
    {
        _valueRenderConverter = valueRenderConverter;
        _valueMapperConverter = valueMapperConverter;
        _state = new DynamicValueObject()
        {
            TriggerId = id,
            InitialValue = initialValue?.ToString(),
        };
    }

    public JavascriptCode Run()
    {
        return new JavascriptCode($"trigger({{Id:'{_state.TriggerId}'}})");
    }
    
    public JavascriptCode Set(ValueMapper<T> valueMapper)
    {
        var valueMapperId = IdUtils.CreateId(valueMapper.Method);
        _state.Add(valueMapperId, _valueMapperConverter(valueMapper));
        return new JavascriptCode($"trigger({{Id: '{_state.TriggerId}', ValueMapperId: '{valueMapperId}', InitialValue: {_state.InitialValue}}})");
    }

    public DynamicHtml On(ValueRender<T> valueRenderGeneric)
    {
        var valueRenderId = IdUtils.CreateId(valueRenderGeneric.Method);
        var valueRender = _valueRenderConverter(valueRenderGeneric);
        var dynamicHtml = new DynamicHtml(_state, valueRenderId, valueRender);
        _state.Add(valueRenderId, dynamicHtml);
        return dynamicHtml;
    }
    
    public JavascriptCode SetFromThisValue()
    {
        return new JavascriptCode($"trigger({{Id: '{_state.TriggerId}', 'value': this.value}})");
    }
}

public interface IDynamicValue<T>
{
    public JavascriptCode Run();
    public JavascriptCode Set(ValueMapper<T> valueMapper);
    public JavascriptCode SetFromThisValue();
    public DynamicHtml On(ValueRender<T> valueRenderGeneric);
}

public class DynamicValueObject
{
    public string TriggerId { get; set; }
    public string? InitialValue { get; set; }
    private readonly Dictionary<string, ValueMapper<string>> ValueMappers = new();
    private readonly Dictionary<string, DynamicHtml> DynamicHtmls = new();

    public void Add(string id, ValueMapper<string> valueMapper)
    {
        ValueMappers[id] = valueMapper;
    }
    
    public void Add(string id, DynamicHtml dynamicHtml)
    {
        DynamicHtmls[id] = dynamicHtml;
    }

    public List<DynamicHtml> GetDynamicHtml()
    {
        return DynamicHtmls.Values.ToList();
    }

    public string MapValue(string id, string value, IServiceProvider serviceProvider)
    {
        return ValueMappers[id](value, serviceProvider);
    }
}