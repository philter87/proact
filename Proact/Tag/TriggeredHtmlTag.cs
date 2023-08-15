namespace Proact.Tag;

public class TriggeredHtmlTag
{
    private readonly TriggerRender<object> _triggerRender;
    public string TriggerId { get; }
    private readonly object? _initialValue;

    public TriggeredHtmlTag(TriggerRender<object> triggerRender, string triggerId, object? initialValue)
    {
        _triggerRender = triggerRender;
        TriggerId = triggerId;
        _initialValue = initialValue;
    }

    public RenderState Render(RenderState renderState, object? value = null)
    {
        var tag = _triggerRender(_initialValue, renderState.ServiceProvider);
        tag.AddAttribute("data-trigger-id", TriggerId);
        tag.Render(renderState);
        return renderState;
    }

    public HtmlTag Create(object value, IServiceProvider serviceProvider)
    {
        var tag = _triggerRender(value, serviceProvider);
        tag.AddAttribute("data-trigger-id", TriggerId);
        return tag;
    }
}