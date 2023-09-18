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

    public override RenderState Render(RenderState renderState)
    {
        return RenderStateValue(renderState, _dynamicValue.InitialValue);
    }
    
    public RenderState RenderStateValue(RenderState renderState, string? value)
    {
        var tag = _valueRender.Invoke(value, renderState.RenderContext);
        tag.Add("data-dynamic-html-id", _dynamicHtmlId);
        tag?.Render(renderState);
        renderState.AddDynamicHtmlTags(this);
        return renderState;
    }

    public string GetValueId()
    {
        return _dynamicValue.Id;
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