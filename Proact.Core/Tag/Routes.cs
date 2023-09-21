using static Proact.Core.Tags;

namespace Proact.Core.Tag;

public static class Routes
{
    public static HtmlTag Create(params Route[] routes)
    {
        var currentRoute = DynamicValue.CreateWithContext("proact-routing", r => r.UrlPath);
        return div().With(currentRoute.Map((v, _) => FindMatchingRoute(routes, v)));
    }

    private static HtmlTag FindMatchingRoute(Route[] routes, string value)
    {
        var route = routes.FirstOrDefault(r => r.Path == value); 
        return route == null ? routes[0].Tag : route.Tag;
    }
}