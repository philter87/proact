using Proact.Core;
using Proact.Core.Tag;
using static Proact.Core.Tags;

namespace ProactTests;

public class TagTest
{
    [Fact]
    public void Pretty_render_with_pretty_is_false()
    {
        var tag = div().With(
            "String within node",
            p("my-class", style: "Style").With("Hello World")
        );

        var html = tag.Render(new RenderState(Any.RenderContext)).GetHtml();
        
        Assert.Equal(@"<div>String within node<p class=""my-class"" style=""Style"">Hello World</p></div>", html);
    }
    
    [Fact]
    public void Static_method_calls_and_new_class_is_the_same()
    {
        var tags = new Div("btn-primary")
        {
            new P()
            {
                "Hello World!"
            },
        };

        var actual = tags.Render(Any.RenderState).GetHtml();
        
        var expected = div("btn-primary").With(p().With("Hello World!")).Render(Any.RenderState).GetHtml();
        Assert.Equal(expected, actual);
    }
}