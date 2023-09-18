
namespace Proact.Core.Tag.Old;


public static class TagsOld
{

    public static HtmlTagFunc h5(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h5", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc head(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("head", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h1(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h1", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc body(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("body", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h3(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h3", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc span(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("span", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h2(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h2", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc p(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("p", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc input(string? klass = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("input", children).Put("class",klass).Put("type",type).Put("name",name).Put("oninput",oninput).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc form(string? klass = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("form", children).Put("class",klass).Put("onsubmit",onsubmit).Put("name",name).Put("action",action).Put("method",method).Put("enctype",enctype).Put("target",target).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc button(string? klass = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("button", children).Put("class",klass).Put("name",name).Put("type",type).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc div(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("div", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h4(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h4", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h6(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h6", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc html(string? klass = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("html", children).Put("class",klass).Put("xmlns",xmlns).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

}


