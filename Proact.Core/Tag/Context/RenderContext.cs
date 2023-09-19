namespace Proact.Core.Tag.Context;

public class RenderContext : IRenderContext
{
    private readonly List<DynamicValueObject> _dynamicValues = new();
    private readonly IServiceProvider _serviceProvider;
    private RenderState _renderState;
    public DynamicValueTriggerOptions? TriggerOptions { get; set; }

    public RenderContext(IServiceProvider serviceProvider, DynamicValueTriggerOptions? triggerOptions = null)
    {
        _serviceProvider = serviceProvider;
        _renderState = new RenderState();
        TriggerOptions = triggerOptions;
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