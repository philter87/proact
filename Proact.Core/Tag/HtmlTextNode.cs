using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class HtmlTextNode : HtmlNode
{
    private readonly string _rawContent;

    public HtmlTextNode(string rawContent)
    {
        _rawContent = rawContent;
    }

    public override RenderContext Render(RenderContext renderContext)
    {
        return renderContext.AddLine(_rawContent);
    }
}