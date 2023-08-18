namespace Proact.Core.Tag;

public class DynamicHtml : HtmlNode
{
    private readonly DynamicValue _dynamicValue;

    public DynamicHtml(DynamicValue dynamicValue)
    {
        _dynamicValue = dynamicValue;
    }

    public override RenderState Render(RenderState renderState)
    {
        return RenderStateValue(renderState, _dynamicValue.InitialValue);
    }
    
    public DynamicHtmlResult? Render(RenderState renderState, object? value)
    {
        var newValue = _dynamicValue.ValueMapper(value, renderState.ServiceProvider);
        renderState = RenderStateValue(renderState, newValue);
        return new DynamicHtmlResult()
        {
            Html = renderState.GetHtml(),
            Value = newValue,
            InitialValue = _dynamicValue.InitialValue,
        };
    }
    
    private RenderState RenderStateValue(RenderState renderState, object? value)
    {
        var tag = _dynamicValue.ValueRender?.Invoke(value, renderState.ServiceProvider);
        tag?.Add("data-trigger-id", _dynamicValue.Id);
        tag?.Render(renderState);
        renderState.AddTriggeredHtmlTag(this);
        return renderState;
    }

    public string GetValueId()
    {
        return _dynamicValue.Id;
    }
}