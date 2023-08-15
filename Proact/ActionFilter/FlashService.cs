using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proact.Tag;

namespace Proact;

public class FlashService : IActionFilter
{
    private readonly IServiceProvider _serviceProvider;
    private const string TriggerBody = "triggerBody";
    private readonly Dictionary<string, TriggeredHtmlTag> _triggeredHtmlTags = new();

    public FlashService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var query = context.HttpContext.Request.Query;
        if (!query.ContainsKey(TriggerBody))
        {
            return;
        }

        var triggerBody = ParseTriggerBody(query);
        if (triggerBody != null && _triggeredHtmlTags.TryGetValue(triggerBody.TriggerId, out var trigger))
        {
            var html = trigger.Render(new RenderState(_serviceProvider), null).GetHtml();
            context.Result = HtmlResult(html);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult { Value: HtmlTag tag })
        {
            foreach (var triggeredHtmlTag in tag.GetTriggeredHtmlTags())
            {
                _triggeredHtmlTags[triggeredHtmlTag.TriggerId] = triggeredHtmlTag;
            }

            var html = tag.Render(_serviceProvider);
            context.Result = HtmlResult(html);
        }
    }
    
    private static ContentResult HtmlResult(string html)
    {
        return new ContentResult
        {
            ContentType = "text/html",
            Content = html
        };
    }

    private TriggerExecutedBody? ParseTriggerBody(IQueryCollection query)
    {
        byte[] data = Convert.FromBase64String(query["triggerBody"]);
        string decodedString = System.Text.Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<TriggerExecutedBody>(decodedString, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }
}