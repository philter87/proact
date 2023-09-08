
using Proact.Core.Tag;

namespace Proact.Core;


public static class Tags
{

    public static HtmlTagFunc body(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("body", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc input(string? klass = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("input", children).Add("class",klass).Add("type",type).Add("name",name).Add("oninput",oninput).Add("value",value).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc p(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("p", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc form(string? klass = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("form", children).Add("class",klass).Add("onsubmit",onsubmit).Add("name",name).Add("action",action).Add("method",method).Add("enctype",enctype).Add("target",target).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc div(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("div", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc h1(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h1", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc span(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("span", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc h3(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h3", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc h6(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h6", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc button(string? klass = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("button", children).Add("class",klass).Add("name",name).Add("type",type).Add("value",value).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc h4(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h4", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc h5(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h5", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc h2(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("h2", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc html(string? klass = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("html", children).Add("class",klass).Add("xmlns",xmlns).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

    public static HtmlTagFunc head(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null)
    {
        return children => new HtmlTag("head", children).Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }

}


public class Body : HtmlTag
{
    public Body(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("body")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class Input : HtmlTag
{
    public Input(string? klass = null, string? type = null, string? name = null, string? oninput = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("input")
    {
        this.Add("class",klass).Add("type",type).Add("name",name).Add("oninput",oninput).Add("value",value).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class P : HtmlTag
{
    public P(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("p")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class Form : HtmlTag
{
    public Form(string? klass = null, string? onsubmit = null, string? name = null, string? action = null, string? method = null, string? enctype = null, string? target = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("form")
    {
        this.Add("class",klass).Add("onsubmit",onsubmit).Add("name",name).Add("action",action).Add("method",method).Add("enctype",enctype).Add("target",target).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class Div : HtmlTag
{
    public Div(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("div")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class H1 : HtmlTag
{
    public H1(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h1")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class Span : HtmlTag
{
    public Span(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("span")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class H3 : HtmlTag
{
    public H3(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h3")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class H6 : HtmlTag
{
    public H6(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h6")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class Button : HtmlTag
{
    public Button(string? klass = null, string? name = null, string? type = null, string? value = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("button")
    {
        this.Add("class",klass).Add("name",name).Add("type",type).Add("value",value).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class H4 : HtmlTag
{
    public H4(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h4")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class H5 : HtmlTag
{
    public H5(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h5")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class H2 : HtmlTag
{
    public H2(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("h2")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class Html : HtmlTag
{
    public Html(string? klass = null, string? xmlns = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("html")
    {
        this.Add("class",klass).Add("xmlns",xmlns).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

public class Head : HtmlTag
{
    public Head(string? klass = null, string? style = null, string? id = null, bool? hidden = null, string? onclick = null) : base("head")
    {
        this.Add("class",klass).Add("style",style).Add("id",id).Add("hidden",hidden).Add("onclick",onclick);
    }
}

