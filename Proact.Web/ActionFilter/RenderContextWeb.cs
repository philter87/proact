using System.Text.Json;
using Proact.Core;
using Proact.Core.Tag.Context;

namespace Proact.ActionFilter;

public static class RenderContextWeb
{
    public static RenderContext CreateWithValue(HttpContext httpContext)
    {
        var renderContext = Create(httpContext);
        var valueChange = ParseTriggerBody(httpContext.Request.Body).Result;
        
        renderContext.Values = new Dictionary<string, ValueChange>()
        {
            { valueChange.Id, valueChange }
        };
        return renderContext;
    }
    
    public static RenderContext Create(HttpContext httpContext)
    {
        return new RenderContext(httpContext.RequestServices, httpContext.Request.Path, new Dictionary<string, ValueChange>());
    }
    
    private static async Task<ValueChange?> ParseTriggerBody(Stream stream)
    {
        var reader = new StreamReader(stream);
        var rawMessage = await reader.ReadToEndAsync();
        return JsonSerializer.Deserialize<ValueChange>(rawMessage, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        });
    }
}