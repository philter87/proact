using Proact.Core.Tag;

namespace Proact.Core;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, DynamicHtml> _dynamicHtml = new();

    public ProactService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DynamicHtmlResult? HandlePartialRender(ValueChangeBody? triggerBody)
    {
        if (triggerBody != null && _dynamicHtml.TryGetValue(triggerBody.TriggerId, out var dynamicHtml))
        {
            var renderState = new RenderState(_serviceProvider);

            return dynamicHtml.Render(renderState, triggerBody.TriggerOptions?.Value);
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
        renderState.DynamicHtmlTags.ForEach(dt => _dynamicHtml[dt.GetValueId()] = dt);
    }
}