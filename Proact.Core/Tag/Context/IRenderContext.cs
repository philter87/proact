namespace Proact.Core.Tag.Context;

public interface IRenderContext
{
    public S? GetService<S>() where S: class;
    public string UrlPath { get; set; }
    //public T Value<T>();
    // public void Navigate();
    // public void TriggerValueChange<T>(DynamicValue<T> value, T newValue);
    // public V Value<V>();
    // public Dictionary<string, string> QueryParameters();
    // public Dictionary<string, string> PathParameters();
    // public void SetHttpCookie();
    // public void SetHttpHeader();
}