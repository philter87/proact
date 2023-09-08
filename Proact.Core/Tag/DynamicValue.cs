using System.Text.Json;

namespace Proact.Core.Tag;
public delegate ValueRender<string> ValueRenderConverter<out T>(ValueRender<T> valueRender);
public delegate ValueMapper<string> ValueMapperConverter<T>(ValueMapper<T> valueMapper);
public delegate HtmlTag ValueRender<in T> (T? value = default, IServiceProvider? serviceProvider = null);

public delegate T ValueMapper<T>(T value, IServiceProvider? serviceProvider = null);

public static class DynamicValue
{
    public static DynamicValue<int> Create(string triggerId, int initialValue)
    {
        ValueMapperConverter<int> mapperConverter = vm => (v, sp) => vm(int.Parse(v), sp).ToString();
        ValueRenderConverter<int> renderConverter = vr => (v, sp) => vr(int.Parse(v ?? "0"), sp);
        return new DynamicValue<int>(triggerId, initialValue, mapperConverter, renderConverter);
    }
    
    public static DynamicValue<T> Create<T>(string triggerId, T? initialValue)
        {
            
            ValueMapperConverter<T> mapperConverter = vm => (v, sp) => vm(v == null ? default : JsonSerializer.Deserialize<T?>(v), sp).ToString();
            ValueRenderConverter<T> renderConverter = vr => (v, sp) => vr(v == null ? default : JsonSerializer.Deserialize<T?>(v), sp);
            return new DynamicValue<T>(triggerId, initialValue, mapperConverter, renderConverter);
        }
    
    public static DynamicValue<string> Create(string triggerId, string initialValue)
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
        return On(valueRenderGeneric, valueRenderId);
    }

    private DynamicHtml On(ValueRender<T> valueRenderGeneric, string valueRenderId)
    {
        var valueRender = _valueRenderConverter(valueRenderGeneric);
        var dynamicHtml = new DynamicHtml(_state, valueRenderId, valueRender);
        _state.Add(valueRenderId, dynamicHtml);
        return dynamicHtml;
    }

    public DynamicHtml On(Func<HtmlTag> valueRender)
    {
        return On((_, _) => valueRender(), IdUtils.CreateId(valueRender.Method));
    }
    
    public DynamicHtml On(Func<T?, HtmlTag> valueRender)
    {
        return On((v, _) => valueRender(v), IdUtils.CreateId(valueRender.Method));
    }
    
    public JavascriptCode SetFromThisValue()
    {
        return new JavascriptCode($"trigger({{Id: '{_state.TriggerId}', 'value': this.value}})");
    }

    public JavascriptCode SetOnSubmit()
    {
        return new JavascriptCode($"proactFormSubmit({{Id: '{_state.TriggerId}'}}, event)");
    }
}

public interface IDynamicValue<T>
{
    public JavascriptCode Run();
    public JavascriptCode Set(ValueMapper<T> valueMapper);
    public JavascriptCode SetFromThisValue();
    public JavascriptCode SetOnSubmit();
    public DynamicHtml On(ValueRender<T> valueRenderGeneric);
    public DynamicHtml On(Func<HtmlTag> valueRender);
    public DynamicHtml On(Func<T?, HtmlTag> valueRender);
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