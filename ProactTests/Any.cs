using Microsoft.Extensions.DependencyInjection;
using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace ProactTests;

public static class Any
{
    public static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();
    public static RenderContext RenderContext => new RenderContext(ServiceProvider);
    public static RenderContext RenderContextWithUrl(string url)
    {
        var renderContext = RenderContext;
        renderContext.UrlPath = url;
        return renderContext;
    }

    public static RenderContext RenderContextWith(string id, string? value, string? valueMapperId = null) => new(ServiceProvider, new ValueChange(id, value, valueMapperId));
    public static RenderState RenderState => new();
    
    
}