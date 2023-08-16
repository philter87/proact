namespace Proact.Core.Tag;

public class HtmlDynamic : HtmlNode
{
    private readonly TriggerRender<object> _triggerRender;
    public string TriggerId { get; }
    private readonly object? _initialValue;

    public HtmlDynamic(TriggerRender<object> triggerRender, string triggerId, object? initialValue)
    {
        _triggerRender = triggerRender;
        TriggerId = triggerId;
        _initialValue = initialValue;
    }

    public override RenderState Render(RenderState renderState)
    {
        return Render(renderState, _initialValue);
    }
    
    public RenderState Render(RenderState renderState, object? value)
    {
        var tag = _triggerRender(value, renderState.ServiceProvider);
        tag?.Add("data-trigger-id", TriggerId);
        tag?.Render(renderState);
        renderState.AddTriggeredHtmlTag(this);
        return renderState;
    }
}