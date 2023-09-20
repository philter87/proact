using static Proact.Core.Tags;

namespace Proact.Core.Tag;

public static class Routes
{
    public static HtmlTag Create(params Route[] routes)
    {
        var currentRoute = DynamicValue.Create("routing", "Irrelevant value");
        return div().With(
            currentRoute.Map((_, c) => routes.First(r => r.Path == c.UrlPath).Tag)
            );
    }
}