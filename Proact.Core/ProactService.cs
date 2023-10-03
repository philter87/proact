using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.Core;

public class ProactService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<string, List<IMappedValue>> _valuesByRootId = new();

    public ProactService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public DynamicHtmlResult? RenderPartial(RenderState renderState)
    {
        var valueChanges = renderState.RenderContext.Values;

        if (valueChanges.Count == 0)
        {
            return null;
        }

        var valueChangeOptions = valueChanges.First().Value;

        if (!_valuesByRootId.ContainsKey(valueChangeOptions.Id))
        {
            return null;
        }
        
        var values = _valuesByRootId[valueChangeOptions.Id];
        return new DynamicHtmlResult()
        {
            HtmlChanges = values.Select(dh =>
                {
                    renderState.ClearHtml();
                    dh.Render(renderState); 
                    AddRootValueChildren(renderState);
                    return new HtmlChange(dh.Id, renderState.GetHtml());
                }).ToList(),
            Value = GetParentValue(values[0], renderState)
        };
    }

    private object GetParentValue(IMappedValue value, RenderState state)
    {
        if (value.Parent == null)
        {
            return Json.AsString(value.GetValue(state.RenderContext));
        }

        return GetParentValue(value.Parent, state);
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

    private void AddAppendNewChildrenToRootValue(IMappedValue value)
    {
        var valuesDependentOnRootValue = _valuesByRootId.GetValueOrDefault(value.RootId, new List<IMappedValue>());

        if (valuesDependentOnRootValue.Exists(v => value.Id == v.Id))
        {
            return;
        }
        
        valuesDependentOnRootValue.Add(value);
        _valuesByRootId[value.RootId] = valuesDependentOnRootValue;
    }

}