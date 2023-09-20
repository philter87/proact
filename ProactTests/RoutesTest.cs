using Proact.Core.Tag;
using static Proact.Core.Tags;

namespace ProactTests;

public class RoutesTest
{
    [Fact]
    public void Find_simple_route()
    {
        var routing = Routes.Create(new Route("", div().With("Home")));

        var route = routing.Render(Any.RenderContextWithUrl(""));

        var html = route.GetHtml();
        Assert.Contains("Home", html);
    }
    
    [Fact]
    public void Find_second_route()
    {
        var routing = Routes.Create(
            new Route("", div().With("Home")),
            new Route("about", div().With("About"))
            );

        var route = routing.Render(Any.RenderContextWithUrl("about"));

        var html = route.GetHtml();
        Assert.Contains("About", html);
    }
}