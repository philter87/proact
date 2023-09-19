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
        var triggerOptions = renderContext.TriggerOptions;
        if (triggerOptions == null)
        {
            return null;
        }

        if (!_dynamicValues.ContainsKey(triggerOptions.Id))
        {
            return null;
        }
        
        var value = triggerOptions.Value;
        var dynamicValue = _dynamicValues[triggerOptions.Id];
        if (triggerOptions.ValueMapperId != null)
        {
            value = dynamicValue.MapValue(triggerOptions.ValueMapperId, value, renderContext);
        }
        
        return new DynamicHtmlResult()
        {
            IdToHtml = dynamicValue.GetDynamicHtml()
                .ToDictionary(dh => dh.GetDynamicHtmlId(), dh =>
                {
                    renderContext.ClearHtml();
                    dh.RenderStateValue(renderContext, value); 
                    CacheHtmlTags(renderContext);
                    return renderContext.GetHtml();
                }),
            Value = value,
            InitialValue = dynamicValue.InitialValue,
        };
    }
    
    public string Render(HtmlTag tag)
    {
        var renderContext = tag.Render(new RenderContext(_serviceProvider));
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