using Microsoft.Extensions.DependencyInjection;
using Proact.Core.Tag;
using Proact.Core.Tag.Change;
using Proact.Core.Tag.Context;

namespace ProactTests;

public static class Any
{
    public static IServiceProvider ServiceProvider => new ServiceCollection().BuildServiceProvider();
    public static IRenderContext RenderContext => new RenderContext(ServiceProvider);
    public static RenderState RenderState => new RenderState(RenderContext);
}