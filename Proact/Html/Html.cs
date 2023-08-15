namespace Proact.Html;

public static class Html
{
    public static HtmlTagFunc p(string? klass = null, string? style = null)
    {
        return children => new HtmlTag("p", children)
            .AddAttribute("class", klass)
            .AddAttribute(nameof(style), style);
    }

    public static HtmlTagFunc div(string? klass = null, string? style = null)
    {
        return children => new HtmlTag("div", children)
            .AddAttribute("class", klass)
            .AddAttribute(nameof(style), style);
    }
    
    public static HtmlTagFunc head(string? klass = null, string? style = null)
    {
        return children => new HtmlTag("head", children)
            .AddAttribute("class", klass)
            .AddAttribute(nameof(style), style);
    }
    
    public static HtmlTagFunc body(string? klass = null, string? style = null)
    {
        return children => new HtmlTag("body", children)
            .AddAttribute("class", klass)
            .AddAttribute(nameof(style), style);
    }

    public static HtmlTagFunc html(string? lang = null)
    {
        return children => new HtmlTag("html", children)
            .AddAttribute(nameof(lang), lang);
    }
    
    public static HtmlTagFunc button(string? klass = null, string? style = null, string? onclick = null)
    {
        return children => new HtmlTag("button", children)
            .AddAttribute("class", klass)
            .AddAttribute(nameof(onclick), onclick)
            .AddAttribute(nameof(style), style);
    }
}