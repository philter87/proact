using System.Text.RegularExpressions;
using Proact.Core;
using Proact.Core.Tag;

namespace ProactTests;

public static class HtmlTestUtils
{
    public static void Equal(string expectedHtmlWithoutId, RenderState renderState)
    {
        Assert.Equal(expectedHtmlWithoutId, RemoveDynamicValueId(renderState.GetHtml()));
    }

    public static void Equal(string expectedHtmlWithoutId, HtmlChange htmlChange)
    {
        Assert.Equal(expectedHtmlWithoutId, RemoveDynamicValueId(htmlChange.Html));
    }
    
    private static string RemoveDynamicValueId(string htmlWithId)
    {
        return Regex.Replace(htmlWithId, " data-dynamic-value-id=\".{8}\"", "");
    }
}