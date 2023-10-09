using System.Text;
using Proact.Core.Tag;

namespace ProactTests;

public class RenderContextUtilsTest
{
    [Fact]
    public void GetQueryParameters_no_query_strings()
    {
        var url = "/about";
        
        var queryParameters = RenderContextUtils.GetQueryParameters(url);
        
        Assert.Empty(queryParameters);
    }
    
    [Fact]
    public void GetQueryParameters_a_single_query_parameter_with_HelloWorld()
    {
        var url = "/about?myParameter=HelloWorld";
        
        var queryParameters = RenderContextUtils.GetQueryParameters(url);

        Assert.Single(queryParameters);
        Assert.Equal("HelloWorld", queryParameters["myParameter"][0]);
    }

    [Fact]
    public void CreateUrl()
    {
        var urlPath = "/philip";
        var qp = new Dictionary<string, string>()
        {
            {"middleName", "Hjorth"},
            {"secondName","Christiansen"}
        };

        var url = RenderContextUtils.CreateUrl(urlPath, qp);
        
        Assert.Equal("/philip?middleName=Hjorth&secondName=Christiansen", url);
    }
}