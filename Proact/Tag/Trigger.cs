using Proact.Html;

namespace Proact.Tag;

public delegate HtmlTag TriggerRender<in T> (T? value = default, IServiceProvider? serviceProvider = null);

public class Trigger<T>
{
    private string Id { get; }
    private readonly T? _value;

    public Trigger(string id)
    {
        Id = id;
        _value = default;
    }
    
    public Trigger(string id, T value)
    {
        Id = id;
        _value = value;
    }

    public JavascriptCode Run()
    {
        return new JavascriptCode($"trigger('{Id}')");
    }

    public HtmlDynamic On(TriggerRender<object> triggerRender)
    {
        return new HtmlDynamic(triggerRender, Id, _value);
    }
}