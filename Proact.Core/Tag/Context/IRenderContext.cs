namespace Proact.Core.Tag.Context;

public interface IRenderContext
{
    public S? GetService<S>() where S: class;
    public IServiceProvider ServiceProvider { get; set; }
    public string CurrentUrlPath { get; set; }

    public string CurrentUrlPattern { get; set; }
    public Dictionary<string, List<string>> QueryParameters { get; set; }

    public Dictionary<string, string> PathParameters { get; set; }

    public Dictionary<string, ValueChangeCommand> ValueChanges { get; set; }
    
    public void TriggerValueChange<T>(RootValue<T> value, T newValue);
    // public void Navigate();
    
    
    // public void SetHttpCookie();
    // public void SetHttpHeader();
}