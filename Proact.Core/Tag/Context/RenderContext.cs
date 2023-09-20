namespace Proact.Core.Tag.Context;

public class RenderContext : IRenderContext
{
    private readonly List<DynamicValueObject> _dynamicValues = new();
    private readonly IServiceProvider _serviceProvider;
    private RenderState _renderState;
    public string UrlPath { get; set; }
    public Dictionary<string, ValueChange> ValueChanges { get; } = new();

    public RenderContext(IServiceProvider serviceProvider, ValueChange? valueChange = null)
    {
        _serviceProvider = serviceProvider;
        _renderState = new RenderState();
        AddTrigger(valueChange);
    }

    private void AddTrigger(ValueChange? triggerOptions)
    {
        if (triggerOptions == null)
        {
            return;
        }
        ValueChanges[triggerOptions.Id] = triggerOptions;
    }

    public ValueChange? GetTriggerOptions(DynamicValueObject dynamicValueObject)
    {
        return ValueChanges.GetValueOrDefault(dynamicValueObject.Id);
    }

    public S? GetService<S>() where S: class
    {
        return (S?) _serviceProvider.GetService(typeof(S));
    }

    internal RenderContext AddLine(string line)
    {
        _renderState.AddLine(line);
        return this;
    }

    internal void AddDynamicHtmlTags(DynamicHtml value)
    {
        _dynamicValues.Add(value.GetValue());
    }

    public List<DynamicValueObject> GetValues()
    {
        return _dynamicValues;
    }

    internal void ClearHtml()
    {
        _renderState = new RenderState();
    }
    

    public string GetHtml()
    {
        return _renderState.GetHtml();
    }
}