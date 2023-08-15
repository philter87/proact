﻿using Proact.Html;

namespace Proact.Tag;

public class HtmlTextNode : HtmlNode
{
    private readonly string _rawContent;

    public HtmlTextNode(string rawContent)
    {
        _rawContent = rawContent;
    }

    public override RenderState Render(RenderState renderState)
    {
        return renderState.AddLine(_rawContent);
    }
}