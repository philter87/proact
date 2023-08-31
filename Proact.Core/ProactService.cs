using Proact.Core.Tag;

namespace Proact.Core;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, DynamicValueObject> _dynamicValues = new();

    public ProactService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DynamicHtmlResult? RenderPartial(TriggerOptions? triggerOptions)
    {
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
            value = dynamicValue.MapValue(triggerOptions.ValueMapperId, value, _serviceProvider);
        }
        
        return new DynamicHtmlResult()
        {
            IdToHtml = dynamicValue.GetDynamicHtml()
                .ToDictionary(dh => dh.GetDynamicHtmlId(), dh => dh.RenderStateValue(new RenderState(_serviceProvider), value).GetHtml()),
            Value = value,
            InitialValue = dynamicValue.InitialValue,
        };
    }
    
    public string Render(HtmlTag tag)
    {
        var renderState = tag.Render(new RenderState(_serviceProvider));
        CacheHtmlTags(renderState);
        return renderState.GetHtml();
    }


    private void CacheHtmlTags(RenderState renderState)
    {
        foreach (var dynamicValueKv in renderState.DynamicValues)
        {
            _dynamicValues[dynamicValueKv.TriggerId] = dynamicValueKv;
        }
    }
}