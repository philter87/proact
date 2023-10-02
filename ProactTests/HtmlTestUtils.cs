using System.Text.RegularExpressions;
using Proact.Core.Tag;

namespace ProactTests;

public static class HtmlTestUtils
{
    public static void Equal(string expectedHtmlWithoutId, RenderState renderState)
    {
        var html = Regex.Replace(renderState.GetHtml(), " data-dynamic-value-id=\".{8}\"", "");
        
        Assert.Equal(expectedHtmlWithoutId, html);
    }
}