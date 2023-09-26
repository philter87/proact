using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;
public delegate ValueRender<string> ValueRenderConverter<out T>(ValueRender<T> valueRender);
public delegate ValueMapper<string> ValueMapperConverter<T>(ValueMapper<T> valueMapper);
public delegate HtmlTag ValueRender<in T> (T value, IRenderContext context);

public delegate T ValueMapper<T>(T value, IRenderContext context);

public static class DynamicValue
{
    public static DynamicValue<T> Create<T>(string id, T initialValue)
    {
        var state = new ValueState(id) { InitialValue = Json.Parse(initialValue) };
        return new DynamicValue<T>(state);
    }
    
    public static DynamicValue<T> CreateWithContext<T>(string id, Func<IRenderContext, T> initialValueCreator)
    {
        var state = new ValueState(id) { InitialValueCreator = r => Json.Parse(initialValueCreator(r)) };
        return new DynamicValue<T>(state);
    }

    public static DynamicValue<string?> CreatePathParameter(string pathParameter)
    {
        return CreateWithContext(pathParameter, c => c.PathParameters.GetValueOrDefault(pathParameter));
    }

    public static DynamicValue<string> CreateQueryParameter(string queryParameter)
    {
        return CreateWithContext(queryParameter,
            c => c.QueryParameters.GetValueOrDefault(queryParameter, new List<string> {""})[0]);
    }
    public static DynamicValue<List<string>> CreateQueryParameters(string queryParameter)
    {
        return CreateWithContext(queryParameter, c => c.QueryParameters.GetValueOrDefault(queryParameter, new List<string>()));
    }
}

public class DynamicValue<T> : IDynamicValue<T>
{
    private readonly ValueRenderConverter<T> _valueRenderConverter = vr => (v, sp) => vr(Json.Parse<T>(v), sp);
    private readonly ValueMapperConverter<T> _valueMapperConverter = vm => (v, sp) => vm(Json.Parse<T>(v), sp).ToString();
    private readonly ValueState _state;

    public DynamicValue(ValueState state)
    {
        _state = state;
    }

    public string Id => _state.Id;

    public JavascriptCode Run()
    {
        return new JavascriptCode($"changeDynamicValue('{_state.Id}')");
    }
    
    public JavascriptCode Set(ValueMapper<T> valueMapper)
    {
        var valueMapperId = IdUtils.CreateId(valueMapper.Method);
        _state.Add(valueMapperId, _valueMapperConverter(valueMapper));
        return new JavascriptCode($"changeDynamicValue('{_state.Id}', undefined, {{ValueMapperId: '{valueMapperId}'}})");
    }
    
    public JavascriptCode Set(Func<T, T> setter)
    {
        var valueMapperId = IdUtils.CreateId(setter.Method);
        _state.Add(valueMapperId, _valueMapperConverter((v, _) => setter(v)));
        return new JavascriptCode($"changeDynamicValue('{_state.Id}', undefined, {{ValueMapperId: '{valueMapperId}'}})");
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
        return new JavascriptCode($"changeDynamicValue('{_state.Id}', this.value)");
    }

    public JavascriptCode SetOnSubmit()
    {
        return new JavascriptCode($"proactFormSubmit('{_state.Id}', event)");
    }
}

public interface IDynamicValue<T>
{
    public string Id { get; }
    public JavascriptCode Run();
    public JavascriptCode Set(ValueMapper<T> valueMapper);
    public JavascriptCode SetFromThisValue();
    public JavascriptCode SetOnSubmit();
    public DynamicHtml Map(ValueRender<T> valueRenderGeneric);
    public DynamicHtml Map(Func<HtmlTag> valueRender);
    public DynamicHtml Map(Func<T?, HtmlTag> valueRender);
}

public class ValueState
{
    public string Id { get; set; }
    public string? InitialValue { get; set; }
    public Func<IRenderContext, string>? InitialValueCreator { get; set; }
    private readonly Dictionary<string, ValueMapper<string>> _valueMappers = new();
    private readonly Dictionary<string, DynamicHtml> _dynamicHtmls = new();

    public ValueState(string id)
    {
        Id = id;
    }

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

    public string MapValue(string id, string? value, IRenderContext renderContext)
    {
        value ??= InitialValue;
        return _valueMappers[id](value, renderContext);
    }
}