
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proact.Core;
using Proact.Core.Tag;

namespace Proact.ActionFilter;

public class ProactActionFilter : IActionFilter
{
    private const string TriggerBody = "triggerBody";
    private readonly ProactService _proactService;

    public ProactActionFilter(ProactService proactService)
    {
        _proactService = proactService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var query = context.HttpContext.Request.Query;
        if (!query.ContainsKey(TriggerBody))
        {
            return;
        }

        var triggerBody = ParseTriggerBody(query);
        context.Result = JsonResult(_proactService.HandlePartialRender(triggerBody));
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // The context is forwarded to the flashService which is a singleton an able to cache results
        if (context.Result is ObjectResult { Value: HtmlTag tag })
        {
            context.Result = HtmlResult(_proactService.HandleFullRender(tag));
        }
    }
    
    private ValueChangeBody? ParseTriggerBody(IQueryCollection query)
    {
        byte[] data = Convert.FromBase64String(query["triggerBody"]);
        string decodedString = System.Text.Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<ValueChangeBody>(decodedString, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });
    }
    
    private static ContentResult? HtmlResult(string? html)
    {
        if (html == null)
        {
            return null;
        }
        return new ContentResult
        {
            ContentType = "text/html",
            Content = html
        };
    }

    private static ContentResult? JsonResult(DynamicHtmlResult? dynamicHtmlResult)
    {
        if (dynamicHtmlResult == null)
        {
            return null;
        }

        return new ContentResult()
        {
            ContentType = "application/json",
            Content = JsonSerializer.Serialize(dynamicHtmlResult),
        };
    }
}