
using Proact.Core.Tag;

namespace Proact.Core;


public static class Tags
{

    public static HtmlTag h3(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h3").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h2(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h2").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag input(string? classes = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("input").Put("class",classes).Put("type",type).Put("name",name).Put("oninput",oninput).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h1(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h1").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag html(string? classes = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("html").Put("class",classes).Put("xmlns",xmlns).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag body(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("body").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag button(string? classes = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("button").Put("class",classes).Put("name",name).Put("type",type).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag p(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("p").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag div(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("div").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h5(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h5").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h4(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h4").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag head(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("head").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag form(string? classes = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("form").Put("class",classes).Put("onsubmit",onsubmit).Put("name",name).Put("action",action).Put("method",method).Put("enctype",enctype).Put("target",target).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h6(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h6").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag span(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("span").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

}



public class H3 : HtmlTag
{
    public H3(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h3")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H2 : HtmlTag
{
    public H2(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h2")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Input : HtmlTag
{
    public Input(string? classes = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("input")
    {
        this.Put("class",classes).Put("type",type).Put("name",name).Put("oninput",oninput).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H1 : HtmlTag
{
    public H1(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h1")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Html : HtmlTag
{
    public Html(string? classes = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("html")
    {
        this.Put("class",classes).Put("xmlns",xmlns).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Body : HtmlTag
{
    public Body(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("body")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Button : HtmlTag
{
    public Button(string? classes = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("button")
    {
        this.Put("class",classes).Put("name",name).Put("type",type).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class P : HtmlTag
{
    public P(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("p")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Div : HtmlTag
{
    public Div(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("div")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H5 : HtmlTag
{
    public H5(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h5")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H4 : HtmlTag
{
    public H4(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h4")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Head : HtmlTag
{
    public Head(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("head")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Form : HtmlTag
{
    public Form(string? classes = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("form")
    {
        this.Put("class",classes).Put("onsubmit",onsubmit).Put("name",name).Put("action",action).Put("method",method).Put("enctype",enctype).Put("target",target).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H6 : HtmlTag
{
    public H6(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h6")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Span : HtmlTag
{
    public Span(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("span")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

