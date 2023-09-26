using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;
public delegate ValueMapper<string> ValueRenderConverter<out T>(ValueMapper<T> valueMapper);
public delegate ValueSetter<string> ValueMapperConverter<T>(ValueSetter<T> valueSetter);
public delegate HtmlTag ValueMapper<in T> (T value, IRenderContext context);

public delegate T ValueSetter<T>(T value, IRenderContext context);

public static class DynamicValue
{
    public static DynamicValue<T> Create<T>(string id, T initialValue)
    {
        return new DynamicValue<T>(id)
        {
            InitialValue = Json.AsString(initialValue),
        };
    }
    
    public static DynamicValue<T> CreateWithContext<T>(string id, Func<IRenderContext, T> initialValueCreator)
    {
        return new DynamicValue<T>(id)
        {
            InitialValueCreator = r => Json.AsString(initialValueCreator(r))
        };
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

public abstract class DynamicValueBase : HtmlNode
{
    public string Id { get; set; }
    public string? InitialValue { get; set; }
    public string ValueRenderId { get; set; }
    
    public string MapValue(string id, string? value, IRenderContext renderContext)
    {
        value ??= InitialValue;
        return value;
        //return _valueMappers[id](value, renderContext);
    }
}

public class DynamicValue<T> : DynamicValueBase, IDynamicValue<T>
{
    private readonly ValueMapper<T> DefaultMapper = (v, c) => new Span() { Json.AsString(v) };
    public Func<IRenderContext, string>? InitialValueCreator { get; set; }
    
    public ValueMapper<T> Mapper { get; set; }

    public DynamicValue(string id)
    {
        Id = id;
        Mapper = DefaultMapper;
        ValueRenderId = id;
    }

    public override RenderState Render(RenderState renderState)
    {
        var triggerOptions = renderState.RenderContext.GetValueChange(Id);
        if (triggerOptions != null)
        {
            return RenderStateValue(renderState, triggerOptions.Value);            
        }

        if (InitialValueCreator != null)
        {
            return RenderStateValue(renderState, InitialValueCreator(renderState.RenderContext));
        }
        return RenderStateValue(renderState, InitialValue);
    }
    
    private RenderState RenderStateValue(RenderState renderState, string? value)
    {
        var tag = Mapper(Json.Parse<T>(value), renderState.RenderContext);
        tag.Put("data-dynamic-value-id", ValueRenderId);
        tag.Render(renderState);
        renderState.AddDynamicHtmlTags(this);
        return renderState;
    }

    public JavascriptCode Run()
    {
        return new JavascriptCode($"changeDynamicValue('{Id}')");
    }

    public DynamicValue<T> Map(ValueMapper<T> valueMapperGeneric)
    {
        var valueRenderId = IdUtils.CreateId(valueMapperGeneric.Method);
        return Map(valueMapperGeneric, valueRenderId);
    }
    
    public DynamicValue<T> Map(Func<T, HtmlTag> valueRender)
    {
        var valueRenderId = IdUtils.CreateId(valueRender.Method);
        return Map((v, c) => valueRender(v), valueRenderId);
    }
    
    public DynamicValue<T> Map(Func<HtmlTag> valueRender)
    {
        var valueRenderId = IdUtils.CreateId(valueRender.Method);
        return Map((v, c) => valueRender(), valueRenderId);
    }
    
    private DynamicValue<T> Map(ValueMapper<T> valueMapperGeneric, string valueRenderId)
    {
        var value = new DynamicValue<T>(Id)
        {
            ValueRenderId = valueRenderId,
            Mapper = valueMapperGeneric,
            InitialValue = InitialValue,
            InitialValueCreator = InitialValueCreator,
        };
        
        return value;
    }
    
    public JavascriptCode SetFromThisValue()
    {
        return new JavascriptCode($"changeDynamicValue('{Id}', this.value)");
    }

    public JavascriptCode SetOnSubmit()
    {
        return new JavascriptCode($"proactFormSubmit('{Id}', event)");
    }
}

public interface IDynamicValue<T>
{
    public JavascriptCode Run();
    public JavascriptCode SetFromThisValue();
    public JavascriptCode SetOnSubmit();
    public DynamicValue<T> Map(ValueMapper<T> valueMapperGeneric);
}