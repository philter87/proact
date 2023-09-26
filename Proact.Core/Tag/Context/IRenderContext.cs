namespace Proact.Core.Tag.Context;

public interface IRenderContext
{
    public S? GetService<S>() where S: class;
    public IServiceProvider ServiceProvider { get; set; }
    public string CurrentUrlPath { get; set; }

    public string CurrentUrlPattern { get; set; }
    public Dictionary<string, List<string>> QueryParameters { get; set; }

    public Dictionary<string, string> PathParameters { get; set; }

    public Dictionary<string, ValueChange> Values { get; set; }

    //public T Value<T>();
    // public void Navigate();
    // public void TriggerValueChange<T>(DynamicValue<T> value, T newValue);
    // public V Value<V>();
    
    // public void SetHttpCookie();
    // public void SetHttpHeader();
}