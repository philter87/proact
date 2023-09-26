namespace Proact.Core.Tag.Context;

public class RenderContext : IRenderContext
{
    private readonly List<ValueState> _dynamicValues = new();
    public IServiceProvider ServiceProvider { get; set; }
    private RenderState _renderState;
    public string CurrentUrlPath { get; set; }
    public string CurrentUrlPattern { get; set; }
    public Dictionary<string, List<string>> QueryParameters { get; set; }
    public Dictionary<string, string> PathParameters { get; set; }
    public Dictionary<string, ValueChange> Values { get; set; }

    public RenderContext(IServiceProvider serviceProvider, string currentUrlPath, Dictionary<string, ValueChange>? values = null)
    {
        ServiceProvider = serviceProvider;
        CurrentUrlPath = currentUrlPath;
        CurrentUrlPattern = currentUrlPath;
        PathParameters = new Dictionary<string, string>();
        QueryParameters = RenderContextUtils.GetQueryParameters(currentUrlPath);
        Values = values ?? new Dictionary<string, ValueChange>();
        _renderState = new RenderState();
    }

    public ValueChange? GetValueChange(ValueState valueState)
    {
        Values.TryGetValue(valueState.Id, out ValueChange? value);
        return value;
    }

    public S? GetService<S>() where S: class
    {
        return (S?) ServiceProvider.GetService(typeof(S));
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

    public List<ValueState> GetValues()
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