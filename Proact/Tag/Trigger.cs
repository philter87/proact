namespace Proact.Tag;

public delegate HtmlTag TriggerRender<in T> (T? value = default, IServiceProvider? serviceProvider = null);

public class Trigger<T>
{
    private string Name { get; }
    private readonly T? _value;

    public Trigger(string name)
    {
        Name = name;
        _value = default;
    }

    public JavascriptCode Run()
    {
        return new JavascriptCode($"trigger('{Name}')");
    }

    public HtmlTag On(TriggerRender<object> triggerRender)
    {
        var htmlTag = new HtmlTag(HtmlTag.TriggerTagName);
        htmlTag.AddTriggeredHtmlTag(new TriggeredHtmlTag(triggerRender, Name, _value));
        return htmlTag;
    }
}