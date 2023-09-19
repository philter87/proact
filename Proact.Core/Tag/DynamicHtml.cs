using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class DynamicHtml : HtmlNode
{
    private readonly DynamicValueObject _dynamicValue;
    private readonly string _dynamicHtmlId;
    private readonly ValueRender<string> _valueRender;

    public DynamicHtml(DynamicValueObject dynamicValue, string dynamicHtmlId, ValueRender<string> valueRender)
    {
        _dynamicValue = dynamicValue;
        _dynamicHtmlId = dynamicHtmlId;
        _valueRender = valueRender;
    }

    public override RenderContext Render(RenderContext renderContext)
    {
        return RenderStateValue(renderContext, _dynamicValue.InitialValue);
    }
    
    public RenderContext RenderStateValue(RenderContext renderContext, string? value)
    {
        var tag = _valueRender.Invoke(value, renderContext);
        tag.Put("data-dynamic-html-id", _dynamicHtmlId);
        tag.Render(renderContext);
        renderContext.AddDynamicHtmlTags(this);
        return renderContext;
    }

    public DynamicValueObject GetValue()
    {
        return _dynamicValue;
    }

    public string GetDynamicHtmlId()
    {
        return _dynamicHtmlId;
    }
}