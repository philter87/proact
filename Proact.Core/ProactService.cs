using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, IMappedValue> _rootValues = new();

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

        if (!_rootValues.ContainsKey(triggerOptions.Id))
        {
            return null;
        }
        
        var dynamicValue = _rootValues[triggerOptions.Id];
        var all = dynamicValue.Children.ToList();
        all.Add(dynamicValue);
        
        return new DynamicHtmlResult()
        {
            HtmlChanges = all.Select(dh =>
                {
                    renderState.ClearHtml();
                    dh.Render(renderState); 
                    AddRootValueChildren(renderState);
                    return new HtmlChange(dh.Id, renderState.GetHtml());
                }).ToList(),
            Value = triggerOptions.Value
        };
    }
    
    public string Render(HtmlTag tag, RenderState? renderState = null)
    {
        renderState ??= new RenderState(new RenderContext(_serviceProvider, "/"));
        
        tag.Render(renderState);
        AddRootValueChildren(renderState);
        return renderState.GetHtml();
    }


    private void AddRootValueChildren(RenderState renderState)
    {
        foreach (var rootValue in renderState.GetValues())
        {
            AddAppendNewChildrenToRootValue(rootValue);
        }
        
    }

    private void AddAppendNewChildrenToRootValue(IMappedValue rootValue)
    {
        var previousRootValue = _rootValues.GetValueOrDefault(rootValue.Id, rootValue);
        var newChildren = FindNewChildren(rootValue, previousRootValue);
        previousRootValue.Children.AddRange(newChildren);
        _rootValues[rootValue.Id] = previousRootValue;
    }

    private static List<IMappedValue> FindNewChildren(IMappedValue rootValue, IMappedValue previousRootValue)
    {
        var newChilden = rootValue.Children.FindAll(r => previousRootValue.Children.All(p => p.Id != r.Id));
        return newChilden;
    }
}