
using Proact.Core.Tag;

namespace Proact.Core;


public static class Tags
{

    public static HtmlTagFunc body(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("body", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc html(string? klass = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("html", children).Put("class",klass).Put("xmlns",xmlns).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc p(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("p", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h2(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h2", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc span(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("span", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h6(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h6", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc div(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("div", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc input(string? klass = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("input", children).Put("class",klass).Put("type",type).Put("name",name).Put("oninput",oninput).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc button(string? klass = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("button", children).Put("class",klass).Put("name",name).Put("type",type).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h4(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h4", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc form(string? klass = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("form", children).Put("class",klass).Put("onsubmit",onsubmit).Put("name",name).Put("action",action).Put("method",method).Put("enctype",enctype).Put("target",target).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h3(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h3", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc head(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("head", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h1(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h1", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

    public static HtmlTagFunc h5(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h5", children).Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }

}



public class Body : HtmlTag
{
    public Body(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("body")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Html : HtmlTag
{
    public Html(string? klass = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("html")
    {
        this.Put("class",klass).Put("xmlns",xmlns).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class P : HtmlTag
{
    public P(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("p")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H2 : HtmlTag
{
    public H2(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h2")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Span : HtmlTag
{
    public Span(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("span")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H6 : HtmlTag
{
    public H6(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h6")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Div : HtmlTag
{
    public Div(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("div")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Input : HtmlTag
{
    public Input(string? klass = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("input")
    {
        this.Put("class",klass).Put("type",type).Put("name",name).Put("oninput",oninput).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Button : HtmlTag
{
    public Button(string? klass = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("button")
    {
        this.Put("class",klass).Put("name",name).Put("type",type).Put("value",value).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H4 : HtmlTag
{
    public H4(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h4")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Form : HtmlTag
{
    public Form(string? klass = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("form")
    {
        this.Put("class",klass).Put("onsubmit",onsubmit).Put("name",name).Put("action",action).Put("method",method).Put("enctype",enctype).Put("target",target).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H3 : HtmlTag
{
    public H3(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h3")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class Head : HtmlTag
{
    public Head(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("head")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H1 : HtmlTag
{
    public H1(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h1")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

public class H5 : HtmlTag
{
    public H5(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h5")
    {
        this.Put("class",klass).Put("style",style).Put("id",id).Put("hidden",hidden).Put("onclick",onclick);
    }
}

