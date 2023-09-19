using System.Text.Json;
using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;
public delegate ValueRender<string> ValueRenderConverter<out T>(ValueRender<T> valueRender);
public delegate ValueMapper<string> ValueMapperConverter<T>(ValueMapper<T> valueMapper);
public delegate HtmlTag ValueRender<in T> (T value, IRenderContext context);

public delegate T ValueMapper<T>(T value, IRenderContext context);

public static class DynamicValue
{
    public static DynamicValue<int> Create(string triggerId, int initialValue)
    {
        ValueMapperConverter<int> mapperConverter = vm => (v, sp) => vm(int.Parse(v), sp).ToString();
        ValueRenderConverter<int> renderConverter = vr => (v, sp) => vr(int.Parse(v), sp);
        return new DynamicValue<int>(triggerId, initialValue, mapperConverter, renderConverter);
    }
    
    public static DynamicValue<bool> Create(string triggerId, bool initialValue)
    {
        ValueMapperConverter<bool> mapperConverter = vm => (v, sp) => vm(bool.Parse(v), sp).ToString();
        ValueRenderConverter<bool> renderConverter = vr => (v, sp) => vr(bool.Parse(v), sp);
        return new DynamicValue<bool>(triggerId, initialValue, mapperConverter, renderConverter);
    }
    
    public static DynamicValue<T> Create<T>(string triggerId, T initialValue)
        {
            
            ValueMapperConverter<T> mapperConverter = vm => (v, sp) => vm(v == null ? default : JsonSerializer.Deserialize<T>(v), sp).ToString();
            ValueRenderConverter<T> renderConverter = vr => (v, sp) => vr(v == null ? default : JsonSerializer.Deserialize<T>(v), sp);
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
            Id = id,
            InitialValue = initialValue?.ToString(),
        };
    }

    public JavascriptCode Run()
    {
        return new JavascriptCode($"changeDynamicValue({{Id:'{_state.Id}'}})");
    }
    
    public JavascriptCode Set(ValueMapper<T> valueMapper)
    {
        var valueMapperId = IdUtils.CreateId(valueMapper.Method);
        _state.Add(valueMapperId, _valueMapperConverter(valueMapper));
        return new JavascriptCode($"changeDynamicValue({{Id: '{_state.Id}', ValueMapperId: '{valueMapperId}', InitialValue: {_state.InitialValue}}})");
    }
    
    public JavascriptCode Set(Func<T, T> setter)
    {
        var valueMapperId = IdUtils.CreateId(setter.Method);
        _state.Add(valueMapperId, _valueMapperConverter((v, _) => setter(v)));
        return new JavascriptCode($"changeDynamicValue({{Id: '{_state.Id}', ValueMapperId: '{valueMapperId}', InitialValue: {_state.InitialValue}}})");
    }

    public DynamicHtml Map(ValueRender<T> valueRenderGeneric)
    {
        var valueRenderId = IdUtils.CreateId(valueRenderGeneric.Method);
        return Map(valueRenderGeneric, valueRenderId);
    }

    public DynamicHtml Map(Func<HtmlTag> valueRender)
    {
        return Map((_, _) => valueRender(), IdUtils.CreateId(valueRender.Method));
    }
    
    public DynamicHtml Map(Func<T, HtmlTag> valueRender)
    {
        return Map((v, _) => valueRender(v), IdUtils.CreateId(valueRender.Method));
    }
    
    private DynamicHtml Map(ValueRender<T> valueRenderGeneric, string valueRenderId)
    {
        var valueRender = _valueRenderConverter(valueRenderGeneric);
        var dynamicHtml = new DynamicHtml(_state, valueRenderId, valueRender);
        _state.Add(valueRenderId, dynamicHtml);
        return dynamicHtml;
    }
    
    public JavascriptCode SetFromThisValue()
    {
        return new JavascriptCode($"changeDynamicValue({{Id: '{_state.Id}', Value: this.value}})");
    }

    public JavascriptCode SetOnSubmit()
    {
        return new JavascriptCode($"proactFormSubmit({{Id: '{_state.Id}'}}, event)");
    }
}

public interface IDynamicValue<T>
{
    public JavascriptCode Run();
    public JavascriptCode Set(ValueMapper<T> valueMapper);
    public JavascriptCode SetFromThisValue();
    public JavascriptCode SetOnSubmit();
    public DynamicHtml Map(ValueRender<T> valueRenderGeneric);
    public DynamicHtml Map(Func<HtmlTag> valueRender);
    public DynamicHtml Map(Func<T?, HtmlTag> valueRender);
}

public class DynamicValueObject
{
    public string Id { get; set; }
    public string? InitialValue { get; set; }
    private readonly Dictionary<string, ValueMapper<string>> _valueMappers = new();
    private readonly Dictionary<string, DynamicHtml> _dynamicHtmls = new();

    public void Add(string id, ValueMapper<string> valueMapper)
    {
        _valueMappers[id] = valueMapper;
    }
    
    public void Add(string id, DynamicHtml dynamicHtml)
    {
        _dynamicHtmls[id] = dynamicHtml;
    }

    public List<DynamicHtml> GetDynamicHtml()
    {
        return _dynamicHtmls.Values.ToList();
    }

    public string MapValue(string id, string value, IRenderContext renderContext)
    {
        return _valueMappers[id](value, renderContext);
    }
}