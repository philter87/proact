using System.Collections;

namespace Proact.Core.Tag;

public class HtmlTag : HtmlNode, IEnumerable<HtmlNode>
{
    private readonly string _tag;
    private Dictionary<string, object> Attributes { get; }
    private readonly List<HtmlNode> _children;

    public HtmlTag(string tag, params HtmlNode[] children)
    {
        _tag = tag;
        _children = children.ToList();
        Attributes = new Dictionary<string, object>();
    }

    public override RenderState Render(RenderState renderState)
    {
        renderState.AddLine($"<{_tag}{CreateAttributes()}>");
        _children.ForEach(childElement => childElement.Render(renderState));
        
        AddScriptsEndOfBody(renderState);
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
        renderState.AddLine(LoadFlashRuntime.FlashJavascriptRuntime);
        renderState.AddLine("\r\n");
        renderState.AddLine(LoadFlashRuntime.DevWebSocketConnection);
        renderState.AddLine("</script>");
    }

    private string CreateAttributes()
    {
        return Attributes.Count == 0 
            ? "" 
            : " " + string.Join(" ", Attributes.Select(kv => kv.Key + "=\"" + kv.Value + "\""));
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public HtmlTag Put(string name, object? value)
    {
        if (value == null)
        {
            return this;
        }
        Attributes[name] = value;
        return this;
    }

    public void Add(HtmlNode child)
    {
        _children.Add(child);
    }
    
    public HtmlTag With(params HtmlNode[] children)
    {
        _children.AddRange(children);
        return this;
    }

    public IEnumerator<HtmlNode> GetEnumerator()
    {
        return _children.GetEnumerator();
    }
}

public delegate HtmlTag HtmlTagFunc (params HtmlNode[] children);