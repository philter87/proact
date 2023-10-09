using System.Text;
using System.Web;

namespace Proact.Core.Tag;

public static class RenderContextUtils
{
    public static Dictionary<string, List<string>> GetQueryParameters(string url)
    {
        var uri = new Uri("http://example.com" + url);
        if (string.IsNullOrEmpty(uri.Query))
        {
            return new Dictionary<string, List<string>>();
        }
        var query = HttpUtility.ParseQueryString(uri.Query);
        return query.AllKeys.ToDictionary(k => k, k => new List<string>(){query[k]});
    }

    public static string CreateUrl(string relativeUrl, Dictionary<string, string> queryParameters)
    {
        var urlBuilder = new StringBuilder(relativeUrl);
        var qpList = queryParameters.ToList();
        
        for (var index = 0; index < qpList.Count; index++)
        {
            var qp = qpList[index];
            urlBuilder.Append(index == 0 ? "?" : "&");
            urlBuilder.Append($"{qp.Key}={qp.Value}");
        }
        return urlBuilder.ToString();
    }
}