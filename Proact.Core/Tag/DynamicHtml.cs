using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class DynamicHtml : HtmlNode
{
    private readonly ValueState _valueState;
    private readonly string _htmlId;
    private readonly ValueRender<string> _render;

    public DynamicHtml(ValueState valueState, string htmlId, ValueRender<string> render)
    {
        _valueState = valueState;
        _htmlId = htmlId;
        _render = render;
    }

    public override RenderContext Render(RenderContext renderContext)
    {
        var triggerOptions = renderContext.GetValueChange(_valueState);
        if (triggerOptions != null)
        {
            return RenderStateValue(renderContext, triggerOptions.Value);            
        }

        if (_valueState.InitialValueCreator != null)
        {
            return RenderStateValue(renderContext, _valueState.InitialValueCreator(renderContext));
        }
        return RenderStateValue(renderContext, _valueState.InitialValue);
    }
    
    private RenderContext RenderStateValue(RenderContext renderContext, string? value)
    {
        var tag = _render.Invoke(value, renderContext);
        tag.Put("data-dynamic-html-id", _htmlId);
        tag.Render(renderContext);
        renderContext.AddDynamicHtmlTags(this);
        return renderContext;
    }

    public ValueState GetValue()
    {
        return _valueState;
    }

    public string GetDynamicHtmlId()
    {
        return _htmlId;
    }
}