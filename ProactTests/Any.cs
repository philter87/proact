using Microsoft.Extensions.DependencyInjection;
using Proact.Core;
using Proact.Core.Tag;
using Proact.Core.Tag.Context;

namespace ProactTests;

public static class Any
{
    private static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();
    public static RenderContext RenderContextDefault => RenderContextWithUrl("/");
    public static RenderState RenderStateDefault => RenderStateWithUrl("/");

    public static RenderContext RenderContextWithUrl(string url) =>
        new(ServiceProvider, url, new Dictionary<string, ValueChange>());
    public static RenderState RenderStateWithUrl(string url) => new(new RenderContext(ServiceProvider, url));
    public static RenderState RenderStateWithValue(string id, string? value, string? valueMapperId = null) => new(
        new RenderContext(ServiceProvider,
            "/",
            new Dictionary<string, ValueChange>()
            {
                { id, new ValueChange(id, value, valueMapperId) }
            })
        );
    public static RenderState RenderState => new(RenderContextDefault);
    
    
}