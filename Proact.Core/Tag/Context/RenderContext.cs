namespace Proact.Core.Tag.Context;

public class RenderContext : IRenderContext
{
    public IServiceProvider ServiceProvider { get; set; }
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
    }

    public ValueChange? GetValueChange(ValueState valueState)
    {
        return Values.GetValueOrDefault(valueState.Id);
    }

    public S? GetService<S>() where S: class
    {
        return (S?) ServiceProvider.GetService(typeof(S));
    }
}