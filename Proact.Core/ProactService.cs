using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, DynamicValueObject> _dynamicValues = new();

    public ProactService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DynamicHtmlResult? RenderPartial(RenderContext renderContext)
    {
        var triggerOptionsMap = renderContext.ValueChanges;

        if (triggerOptionsMap.Count == 0)
        {
            return null;
        }

        var triggerOptions = triggerOptionsMap.First().Value;

        if (!_dynamicValues.ContainsKey(triggerOptions.Id))
        {
            return null;
        }
        
        var dynamicValue = _dynamicValues[triggerOptions.Id];
        if (triggerOptions.ValueMapperId != null)
        {
            triggerOptions.Value = dynamicValue.MapValue(triggerOptions.ValueMapperId, triggerOptions.Value, renderContext);
        }
        
        return new DynamicHtmlResult()
        {
            HtmlChanges = dynamicValue.GetDynamicHtml()
                .Select(dh =>
                {
                    renderContext.ClearHtml();
                    dh.Render(renderContext); 
                    CacheHtmlTags(renderContext);
                    return new HtmlChange(dh.GetDynamicHtmlId(), renderContext.GetHtml());
                }).ToList(),
            Value = triggerOptions.Value,
            InitialValue = dynamicValue.InitialValue,
        };
    }
    
    public string Render(HtmlTag tag, RenderContext? renderContext = null)
    {
        renderContext ??= tag.Render(new RenderContext(_serviceProvider));
        CacheHtmlTags(renderContext);
        return renderContext.GetHtml();
    }


    private void CacheHtmlTags(RenderContext renderState)
    {
        
        foreach (var dynamicValue in renderState.GetValues())
        {
            _dynamicValues[dynamicValue.Id] = dynamicValue;
        }
    }
}