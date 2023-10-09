using Proact.Core.Tag;
using Proact.Core.Value;
using static Proact.Core.Tags;

namespace ProactTests;

public class RoutesTest
{
    [Fact]
    public void Find_simple_route()
    {
        var routing = Routes.Create(new Route("", div().With("Home")));

        var route = routing.Render(Any.RenderStateWithUrl(""));

        var html = route.GetHtml();
        Assert.Contains("Home", html);
    }
    
    [Fact]
    public void Find_second_route()
    {
        var routing = Routes.Create(
            new Route("", div().With("Home")),
            new Route("/about", div().With("About"))
            );

        var route = routing.Render(Any.RenderStateWithUrl("/about"));

        var html = route.GetHtml();
        Assert.Contains("About", html);
    }
    
    [Fact]
    public void Use_query_parameter()
    {
        var url = "/home?myQueryParameter=HelloWorld";
        var routing = Routes.Create(
            new Route("/home", PageWithQueryParameter("myQueryParameter"))
        );

        var route = routing.Render(Any.RenderStateWithUrl(url));
        
        Assert.Contains("HelloWorld", route.GetHtml());
    }
    
    private static HtmlTag PageWithQueryParameter(string queryParameterName)
    {
        var queryParameter = DynamicValue.CreateQueryParameter(queryParameterName);
        return div().With("Home", queryParameter);
    }
    
    [Fact]
    public void Use_path_parameter()
    {
        var url = "/page/123123";
        var routing = Routes.Create(
            new Route("/", div().With("HOME")),
            new Route("/page/{pageNumber}", PageWithPathParameter())
        );

        var route = routing.Render(Any.RenderStateWithUrl(url));
        
        Assert.Contains("123123", route.GetHtml());
    }

    

    private static HtmlTag PageWithPathParameter()
    {
        var pageNumber = DynamicValue.CreatePathParameter("pageNumber");
        return div().With("Page", pageNumber);
    }
}
