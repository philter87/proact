using Proact.Core.Tag;
using Proact.Core.Tag.Change;

namespace Proact.Core;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, DynamicValueObject> _dynamicValues = new();

    public ProactService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DynamicHtmlResult? RenderPartial(DynamicValueTriggerOptions? triggerOptions)
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
        var renderContext = new RenderContext(_serviceProvider);
        if (triggerOptions.ValueMapperId != null)
        {
            value = dynamicValue.MapValue(triggerOptions.ValueMapperId, value, renderContext);
        }
        
        return new DynamicHtmlResult()
        {
            IdToHtml = dynamicValue.GetDynamicHtml()
                .ToDictionary(dh => dh.GetDynamicHtmlId(), dh => dh.RenderStateValue(new RenderState(renderContext), value).GetHtml()),
            Value = value,
            InitialValue = dynamicValue.InitialValue,
        };
    }
    
    public string Render(HtmlTag tag)
    {
        var renderState = tag.Render(new RenderState(new RenderContext(_serviceProvider)));
        CacheHtmlTags(renderState);
        return renderState.GetHtml();
    }


    private void CacheHtmlTags(RenderState renderState)
    {
        foreach (var dynamicValueKv in renderState.DynamicValues)
        {
            _dynamicValues[dynamicValueKv.Id] = dynamicValueKv;
        }
    }
}