using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.Json;
using Proact.ActionFilter;
using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace Proact.MinimalApi;

// Minimal API 2ms vs MVC controller 30ms
public static class ProactMinimalApi
{
    public static IEndpointConventionBuilder MapProact(
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string pattern,
        Func<IRenderContext, HtmlTag> renderFunc)
    {
        endpoints.MapGet(pattern, async context =>
        {
            var proactService = context.RequestServices.GetRequiredService<ProactService>();
            var renderContext = RenderContextWeb.Create(context);
            var renderState = new RenderState(renderContext);
            var htmlTag = proactService.Render(renderFunc(renderContext), renderState);
            context.Response.ContentType = "text/html";
            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(htmlTag));
            
        });
        
        return endpoints.MapPost(pattern, async context =>
        {
            var proactService = context.RequestServices.GetRequiredService<ProactService>();
            var renderState = new RenderState(RenderContextWeb.CreateWithValue(context));
            var jsonResult = proactService.RenderPartial(renderState);
            var text = JsonSerializer.Serialize(jsonResult);
            context.Response.ContentType = "application/json";
            context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(text));
        }); 
    }
    
    public static IEndpointConventionBuilder MapProact(
        this IEndpointRouteBuilder endpoints,
        [StringSyntax("Route")] string pattern,
        Func<HtmlTag> renderFunc)
    {
        return MapProact(endpoints, pattern, context => renderFunc());
    }

    public static IApplicationBuilder UseHotReload(this IApplicationBuilder app, Func<HttpContext, RequestDelegate, Task> middleware)
    {
        return app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var buffer = new byte[1024 * 4];
                    var receiveResult = await webSocket.ReceiveAsync(
                        new ArraySegment<byte>(buffer), CancellationToken.None);
                    while (!receiveResult.CloseStatus.HasValue)
                    {
                        await webSocket.SendAsync(
                            new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                            receiveResult.MessageType,
                            receiveResult.EndOfMessage,
                            CancellationToken.None);
                
                        receiveResult = await webSocket.ReceiveAsync(
                            new ArraySegment<byte>(buffer), CancellationToken.None);
                    }
                    await webSocket.CloseAsync(receiveResult.CloseStatus.Value, receiveResult.CloseStatusDescription, CancellationToken.None);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                }
            }
            else
            {
                await next(context);
            }
        });
    }
}