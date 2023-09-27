using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class MappedValue<TInput, TReturn>
{
    public Func<TInput, IRenderContext, TReturn> ValueMapper { get; set; }
    public string ValueMapperId { get; set; }

    public RenderState RenderValue(RenderState renderState, TInput renderValue)
    {
        var mappedValue = ValueMapper(renderValue, renderState.RenderContext);
        var htmlNode = MapToTag(mappedValue);
        
        htmlNode.Put("data-dynamic-value-id", ValueMapperId);
        htmlNode.Render(renderState);
        return renderState;
    }

    private HtmlTag MapToTag(TReturn mappedValue)
    {
        if (mappedValue is HtmlTag tag)
        {
            return tag;
        }
        
        return new Span { Json.AsString(mappedValue) };
    }
    
    
    public MappedValue<TReturn, TMappedValue> Map<TMappedValue>(Func<TReturn, IRenderContext, TMappedValue> valueMapper)
    {
        var valueRenderId = IdUtils.CreateId(valueMapper.Method);
        return new MappedValue<TReturn, TMappedValue>()
        {
            ValueMapperId = valueRenderId,
            ValueMapper = valueMapper,
        };
    }
}