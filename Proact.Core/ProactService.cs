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

    public List<ValueChangeRender> RenderPartial(RenderState renderState)
    {
        var clientChanges = renderState.RenderContext.ValueChanges
            .Where(vc => _valuesByRootId.ContainsKey(vc.Key))
            .Select(vc => RenderValueChange(vc.Value, renderState))
            .ToList();

        var serverChanges = renderState.RenderContext.ServerValueChanges
            .Select(command => RenderValueChange(command, renderState))
            .ToList();

        return clientChanges.Concat(serverChanges).ToList();
    }

    private ValueChangeRender RenderValueChange(ValueChangeCommand valueChangeOptions, RenderState renderState)
    {
        var values = _valuesByRootId[valueChangeOptions.Id];

        var parent = FindRoot(values[0]);
        
        return new ValueChangeRender()
        {
            Changes = values.Select(dh =>
            {
                renderState.ClearHtml();
                dh.Render(renderState);
                AddRootValueChildren(renderState);
                return new HtmlChange(dh.Id, renderState.GetHtml());
            }).ToList(),
            Value = Json.AsString(parent.GetValue(renderState.RenderContext))
        };
    }

    private IMappedValue FindRoot(IMappedValue value)
    {
        return value.Parent == null ? value : FindRoot(value.Parent);
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
        foreach (var dynamicValue in renderState.GetDynamicValues())
        {
            AddAppendNewChildrenToRootValue(dynamicValue);
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