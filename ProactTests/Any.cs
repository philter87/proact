using Microsoft.Extensions.DependencyInjection;
using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace ProactTests;

public static class Any
{
    public static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();
    public static RenderContext RenderContext => new RenderContext(ServiceProvider, "/", new Dictionary<string, ValueChange>());
    public static RenderContext RenderContextWithUrl(string url) => new RenderContext(ServiceProvider, url);
    public static RenderContext RenderContextWith(string id, string? value, string? valueMapperId = null) => new(
        ServiceProvider,
        "/",
        new Dictionary<string, ValueChange>()
        {
            { id, new ValueChange(id, value, valueMapperId) }
        });
    public static RenderState RenderState => new();
    
    
}