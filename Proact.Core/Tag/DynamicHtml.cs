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

    public override RenderState Render(RenderState renderState)
    {
        var triggerOptions = renderState.RenderContext.GetValueChange(_valueState);
        if (triggerOptions != null)
        {
            return RenderStateValue(renderState, triggerOptions.Value);            
        }

        if (_valueState.InitialValueCreator != null)
        {
            return RenderStateValue(renderState, _valueState.InitialValueCreator(renderState.RenderContext));
        }
        return RenderStateValue(renderState, _valueState.InitialValue);
    }
    
    private RenderState RenderStateValue(RenderState renderState, string? value)
    {
        var tag = _render.Invoke(value, renderState.RenderContext);
        tag.Put("data-dynamic-html-id", _htmlId);
        tag.Render(renderState);
        renderState.AddDynamicHtmlTags(this);
        return renderState;
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