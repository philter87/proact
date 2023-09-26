﻿using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, ValueState> _dynamicValues = new();

    public ProactService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DynamicHtmlResult? RenderPartial(RenderState renderState)
    {
        var triggerOptionsMap = renderState.RenderContext.Values;

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
            triggerOptions.Value = dynamicValue.MapValue(triggerOptions.ValueMapperId, triggerOptions.Value, renderState.RenderContext);
        }
        
        return new DynamicHtmlResult()
        {
            HtmlChanges = dynamicValue.GetDynamicHtml()
                .Select(dh =>
                {
                    renderState.ClearHtml();
                    dh.Render(renderState); 
                    CacheHtmlTags(renderState);
                    return new HtmlChange(dh.GetDynamicHtmlId(), renderState.GetHtml());
                }).ToList(),
            Value = triggerOptions.Value
        };
    }
    
    public string Render(HtmlTag tag, RenderState? renderState = null)
    {
        renderState ??= new RenderState(new RenderContext(_serviceProvider, "/"));
        
        tag.Render(renderState);
        CacheHtmlTags(renderState);
        return renderState.GetHtml();
    }


    private void CacheHtmlTags(RenderState renderState)
    {
        
        foreach (var dynamicValue in renderState.GetValues())
        {
            _dynamicValues[dynamicValue.Id] = dynamicValue;
        }
    }
}