using System.Collections;
using Proact.Core.Tag.Context;

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

    public override RenderContext Render(RenderContext renderContext)
    {
        renderContext.AddLine($"<{_tag}{CreateAttributes()}>");
        _children.ForEach(childElement => childElement.Render(renderContext));
        
        AddScriptsEndOfBody(renderContext);
        renderContext.AddLine($"</{_tag}>");
        return renderContext;
    }

    private void AddScriptsEndOfBody(RenderContext renderContext)
    {
        if (_tag != "body")
        {
            return;
        }
        
        renderContext.AddLine("<script>");
        renderContext.AddLine(LoadFlashRuntime.FlashJavascriptRuntime);
        renderContext.AddLine("</script>");
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