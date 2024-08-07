
using Proact.Core.Tag;

namespace Proact.Core;


public static class Tags
{

    public static HtmlTag meta(string? classes = null, string? name = null, string? content = null, string? charset = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("meta").Put("class",classes).Put("name",name).Put("content",content).Put("charset",charset).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h4(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h4").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag button(string? classes = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("button").Put("class",classes).Put("name",name).Put("type",type).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag body(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("body").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag div(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("div").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag script(string? classes = null, string? type = null, string? src = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("script").Put("class",classes).Put("type",type).Put("src",src).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag html(string? classes = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("html").Put("class",classes).Put("xmlns",xmlns).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag head(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("head").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag form(string? classes = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("form").Put("class",classes).Put("onsubmit",onsubmit).Put("name",name).Put("action",action).Put("method",method).Put("enctype",enctype).Put("target",target).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h1(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h1").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h2(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h2").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag header(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("header").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag a(string? classes = null, string? href = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("a").Put("class",classes).Put("href",href).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag nav(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("nav").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag link(string? classes = null, string? href = null, string? rel = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("link").Put("class",classes).Put("href",href).Put("rel",rel).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h3(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h3").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag label(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("label").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag span(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("span").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h6(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h6").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag h5(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("h5").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag p(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("p").Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTag input(string? classes = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return new HtmlTag("input").Put("class",classes).Put("type",type).Put("name",name).Put("oninput",oninput).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

}



public class Meta : HtmlTag
{
    public Meta(string? classes = null, string? name = null, string? content = null, string? charset = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("meta")
    {
        this.Put("class",classes).Put("name",name).Put("content",content).Put("charset",charset).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H4 : HtmlTag
{
    public H4(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h4")
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

public class Body : HtmlTag
{
    public Body(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("body")
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

public class Script : HtmlTag
{
    public Script(string? classes = null, string? type = null, string? src = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("script")
    {
        this.Put("class",classes).Put("type",type).Put("src",src).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Html : HtmlTag
{
    public Html(string? classes = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("html")
    {
        this.Put("class",classes).Put("xmlns",xmlns).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
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

public class H1 : HtmlTag
{
    public H1(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h1")
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

public class Header : HtmlTag
{
    public Header(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("header")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class A : HtmlTag
{
    public A(string? classes = null, string? href = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("a")
    {
        this.Put("class",classes).Put("href",href).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Nav : HtmlTag
{
    public Nav(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("nav")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Link : HtmlTag
{
    public Link(string? classes = null, string? href = null, string? rel = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("link")
    {
        this.Put("class",classes).Put("href",href).Put("rel",rel).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H3 : HtmlTag
{
    public H3(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h3")
    {
        this.Put("class",classes).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Label : HtmlTag
{
    public Label(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("label")
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

public class H6 : HtmlTag
{
    public H6(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h6")
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

public class P : HtmlTag
{
    public P(string? classes = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("p")
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

