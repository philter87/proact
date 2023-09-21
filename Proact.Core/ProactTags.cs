using Proact.Core.Tag;

namespace Proact.Core;

public static class ProactTags
{
    public static HtmlTag Link(string? classes = null, string? href = null, string? style = null, string? id = null, bool? hidden = null)
    {
        return new HtmlTag("a").Put("class",classes).Put("href",href).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick", $"proactNavigate('{href}', event);");
    }
}