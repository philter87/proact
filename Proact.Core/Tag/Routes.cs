using Proact.Core.Tag.Context;
using static Proact.Core.Tags;

namespace Proact.Core.Tag;

public static class Routes
{
    private const string DefaultPathToBeIgnored = "DefaultPathToBeIgnored";
    
    public static HtmlTag Create(params Route[] routes)
    {
        var currentRoute = DynamicValue.Create("proact-routing", DefaultPathToBeIgnored);
        return div().With(currentRoute.Map((v, c) => FindMatchingRoute(routes, v, c)));
    }

    private static HtmlTag FindMatchingRoute(Route[] routes, string value, IRenderContext c)
    {
        Console.WriteLine(value + " " +  c.UrlPath);
        value = IsFirstRender(value) ? c.UrlPath : value;
        var route = routes.FirstOrDefault(r => r.Path == value); 
        return route == null ? routes[0].Tag : route.Tag;
    }

    private static bool IsFirstRender(string value)
    {
        return value == DefaultPathToBeIgnored;
    }
}