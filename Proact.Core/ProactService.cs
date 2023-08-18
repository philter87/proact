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

    public DynamicHtmlResult? HandlePartialRender(TriggerOptions? triggerOptions)
    {
        if (triggerOptions != null && _dynamicHtml.TryGetValue(triggerOptions.Id, out var dynamicHtml))
        {
            var renderState = new RenderState(_serviceProvider);

            return dynamicHtml.Render(renderState, triggerOptions.Value);
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