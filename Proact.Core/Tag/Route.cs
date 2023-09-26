using Proact.Core.Tag.Context;

namespace Proact.Core.Tag;

public class Route
{
    public string UrlPattern { get; set; }
    public HtmlTag Tag { get; set; }
    public Func<IRenderContext, HtmlTag> Render { get; set; }
    
    public Route(string urlPattern, HtmlTag tag)
    {
        UrlPattern = urlPattern;
        Tag = tag;
    }

    public RouteMatchResult IsMatched(string currentUrl)
    {
        var uri = new Uri("http://example.com" + currentUrl);
        if (UrlPattern == uri.AbsolutePath)
        {
            return new RouteMatchResult(this, true);    
        }

        var patternSegments = UrlPattern.Split("/");
        var urlSegments = uri.AbsolutePath.Split("/");
        if (patternSegments.Length != urlSegments.Length)
        {
            return new RouteMatchResult(this, false); 
        }

        var pathParameters = new Dictionary<string, string>();
        for (var index = 0; index < patternSegments.Length; index++)
        {
            var patternSegment = patternSegments[index];
            var urlSegment = urlSegments[index];
            if (IsPathParameter(patternSegment))
            {
                pathParameters[RemoveBrackets(patternSegment)] = urlSegment;
                continue;
            }
            if (patternSegment != urlSegment)
            {
                return new RouteMatchResult(this, false);
            }
        }

        return new RouteMatchResult(this, true, pathParameters);
    }

    private static string RemoveBrackets(string patternSegment)
    {
        return patternSegment.Substring(1, patternSegment.Length - 2);
    }

    private static bool IsPathParameter(string urlPatternSegment)
    {
        return urlPatternSegment.StartsWith("{") && urlPatternSegment.EndsWith("}");
    }
}