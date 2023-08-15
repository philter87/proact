using Proact.Tag;

namespace Proact.Html;

public class HtmlTag : HtmlNode
{
    private readonly string _tag;
    private readonly Dictionary<string, object> _attributes;
    private readonly List<HtmlNode> _children;

    public HtmlTag(string tag, params HtmlNode[] children)
    {
        _tag = tag;
        _attributes = new Dictionary<string, object>();
        _children = children.ToList();
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

    public HtmlTag AddChildren(List<HtmlNode> children)
    {
        _children.AddRange(children);
        return this;
    }

    public override RenderState Render(RenderState renderState)
    {
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
}

public delegate HtmlTag HtmlTagFunc (params HtmlNode[] children);