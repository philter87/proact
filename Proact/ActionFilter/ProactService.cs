using Proact.Html;
using Proact.Tag;

namespace Proact;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, HtmlDynamic> _triggeredHtmlTags = new();

    public ProactService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public string? HandlePartialRender(TriggerExecutedBody? triggerBody)
    {
        if (triggerBody != null && _triggeredHtmlTags.TryGetValue(triggerBody.TriggerId, out var trigger))
        {
            var renderState = new RenderState(_serviceProvider);
            return trigger.Render(renderState, triggerBody.Value).GetHtml();
        }

        return null;
    }
    
    public string HandleFullRender(HtmlTag tag)
    {
        var renderState = tag.Render(new RenderState(_serviceProvider));
        CacheHtmlTags(renderState);
        return renderState.GetHtml();
    }


    private void CacheHtmlTags(RenderState renderState)
    {
        renderState.TriggeredHtmlTags.ForEach(t => _triggeredHtmlTags[t.TriggerId] = t);
    }
}