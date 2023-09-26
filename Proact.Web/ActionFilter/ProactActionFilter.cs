﻿using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Proact.ActionFilter;
using Proact.Core;
using Proact.Core.Tag;

namespace Proact.Web.ActionFilter;

public class ProactActionFilter : IActionFilter
{
    private const string ValueChangeRequest = "ValueChangeRequest";
    private readonly ProactService _proactService;

    public ProactActionFilter(ProactService proactService)
    {
        _proactService = proactService;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.HttpContext.Request.Method != "POST" && !context.HttpContext.Request.Query.ContainsKey(ValueChangeRequest))
        {
            return;
        }
        
        var renderContext = RenderContextWeb.CreateWithValue(context.HttpContext);
        var jsonResult = _proactService.RenderPartial(renderContext);
        context.Result = JsonResult(jsonResult);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (IsHtmlRequest(context) && context.Result is ObjectResult { Value: HtmlTag tag })
        {
            var renderContext = RenderContextWeb.Create(context.HttpContext);
            var html = _proactService.Render(tag, tag.Render(renderContext));
            context.Result = HtmlResult(html);
        }
    }

    private static bool IsHtmlRequest(ActionExecutedContext context)
    {
        return (context.HttpContext.Request.Headers.Accept + "").Contains("text/html");
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