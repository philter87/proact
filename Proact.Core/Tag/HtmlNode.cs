using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public abstract class HtmlNode
{
    public abstract RenderContext Render(RenderContext renderContext);
    
    public static implicit operator HtmlNode(string text) => new HtmlTextNode(text);
    public static implicit operator HtmlNode(HtmlTagFunc func) => func();
    public static implicit operator HtmlNode(List<HtmlNode> children) => new HtmlTag("span", children.ToArray());
    public static implicit operator HtmlNode(DynamicValue<string> value) => value.Map(v => Tags.span().With(v));
    public static implicit operator HtmlNode(DynamicValue<int> value) => value.Map(v => Tags.span().With(v+""));
    
}