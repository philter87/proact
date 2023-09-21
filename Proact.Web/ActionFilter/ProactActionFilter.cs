
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.ActionFilter;

public class ProactActionFilter : IActionFilter
{
    private const string ValueChangeRequest = "ValueChangeRequest";
    private readonly ProactService _proactService;
    private readonly IServiceProvider _serviceProvider;

    public ProactActionFilter(ProactService proactService, IServiceProvider serviceProvider)
    {
        _proactService = proactService;
        _serviceProvider = serviceProvider;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Method != "POST" && !context.HttpContext.Request.Query.ContainsKey(ValueChangeRequest))
        {
            return;
        }
        
        var renderContext = new RenderContext(_serviceProvider, ParseTriggerBody(context.HttpContext.Request.Body).Result);
        var jsonResult = _proactService.RenderPartial(renderContext);
        context.Result = JsonResult(jsonResult);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        // The context is forwarded to the flashService which is a singleton an able to cache results
        if (context.Result is ObjectResult { Value: HtmlTag tag })
        {
            var renderContext = new RenderContext(_serviceProvider);
            renderContext.UrlPath = context.HttpContext.Request.Path;
            var html = _proactService.Render(tag, tag.Render(renderContext));
            context.Result = HtmlResult(html);
        }
    }
    
    private static async Task<ValueChange?> ParseTriggerBody(Stream stream)
    {
        var reader = new StreamReader(stream);
        var rawMessage = await reader.ReadToEndAsync();
        
        return JsonSerializer.Deserialize<ValueChange>(rawMessage, new JsonSerializerOptions()
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