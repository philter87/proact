using Proact.Core.Tag.Context;
using Proact.Core.Value;
using static Proact.Core.Tags;

namespace Proact.Core.Tag;

public static class Routes
{
    public static HtmlTag Create(params Route[] routes)
    {
        var currentRoute = DynamicValue.CreateWithContext(Constants.RouteUrlValueId, r => r.CurrentUrlPath);
        return div().With(currentRoute.Map((currentPath, c) => FindMatchingRoute(routes, currentPath, c)));
    }

    private static HtmlTag FindMatchingRoute(Route[] routes, string currentPath, IRenderContext renderContext)
    {
        var matchingRoute = routes
            .Select(r => r.GetMatchResult(currentPath))
            .Where(r => r.IsMatched)
            .FirstOrDefault(new RouteMatchResult(routes[0], false));
        
        renderContext.CurrentUrlPattern = matchingRoute.Route.UrlPattern;
        renderContext.CurrentUrlPath = currentPath;
        renderContext.PathParameters = matchingRoute.PathParameters;
        return matchingRoute.Route.Tag;
    }
}