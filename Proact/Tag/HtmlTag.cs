namespace Proact.Tag;

public class HtmlTag
{
    public const string TriggerTagName = "triggered-tag";
    private readonly string _tag;
    private readonly Dictionary<string, object> _attributes;
    private readonly List<HtmlTag> _children;
    private readonly string _rawContent;

    private TriggeredHtmlTag? _triggeredHtmlTag;

    public HtmlTag(string tag, params HtmlTag[] children)
    {
        _tag = tag;
        _attributes = new Dictionary<string, object>();
        _children = children.ToList();
        _rawContent = "";
    }
    
    public List<TriggeredHtmlTag> GetTriggeredHtmlTags()
    {
        return AddEventsRecursively(new List<TriggeredHtmlTag>());
    }
    
    private HtmlTag(string tag, string rawContent)
    {
        _tag = tag;
        _attributes = new Dictionary<string, object>();
        _children = new List<HtmlTag>();
        _rawContent = rawContent;
    }
    
    public HtmlTag AddAttribute(string name, string? value)
    {
        if (value == null)
        {
            return this;
        }
        _attributes[name] = value;
        return this;
    }

    private HtmlTag AddChildren(List<HtmlTag> children)
    {
        _children.AddRange(children);
        return this;
    }

    public void AddTriggeredHtmlTag(TriggeredHtmlTag trigger)
    {
        _triggeredHtmlTag = trigger;
    }
    private List<TriggeredHtmlTag> AddEventsRecursively(List<TriggeredHtmlTag> events)
    {
        if (_triggeredHtmlTag != null)
        {
            events.Add(_triggeredHtmlTag);
        }

        if (_children.Count == 0)
        {
            return events;
        }
        return _children
            .SelectMany(c => c.AddEventsRecursively(events))
            .ToList();
    }
    
    public string Render(IServiceProvider serviceProvider)
    {
        return Render(new RenderState(serviceProvider)).GetHtml();
    }

    public RenderState Render(RenderState renderState)
    {
        if (_tag == "text-container")
        {
            return renderState.AddLine(_rawContent);
        }

        if (_triggeredHtmlTag != null)
        {
            return _triggeredHtmlTag.Render(renderState);
        }
        
        renderState.AddLine($"<{_tag}{CreateAttributes()}>");
        renderState.LevelIncrement();
        _children.ForEach(childElement => childElement.Render(renderState));
        
        AddScriptsEndOfBody(renderState);
        
        renderState.LevelDecrement();
        renderState.AddLine($"</{_tag}>");
        return renderState;
    }

    private void AddScriptsEndOfBody(RenderState renderState)
    {
        if (_tag != "body")
        {
            return;
        }
        
        renderState.AddLine("<script>");
        renderState.Add(LoadFlashRuntime.FlashJavascriptRuntime);
        renderState.AddLine("</script>");
    }

    private string CreateAttributes()
    {
        return _attributes.Count == 0 
            ? "" 
            : " " + string.Join(" ", _attributes.Select(kv => kv.Key + "=\"" + kv.Value + "\""));
    }

    public static implicit operator HtmlTag(string text) => new("text-container", text);
    public static implicit operator HtmlTag(HtmlTagFunc func) => func();
    public static implicit operator HtmlTag(List<HtmlTag> children) => new HtmlTag("span")
        .AddChildren(children);
}

public delegate HtmlTag HtmlTagFunc (params HtmlTag[] children);