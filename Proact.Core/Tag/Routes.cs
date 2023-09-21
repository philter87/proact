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
        value = value == DefaultPathToBeIgnored ? c.UrlPath : value;
        return routes.First(r => r.Path == value).Tag;
    }
}