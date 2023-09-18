using Proact.Core.Tag.Context;

namespace Proact.Core.Tag.Change;

public class RenderContext : IRenderContext
{
    private readonly IServiceProvider _serviceProvider;

    public RenderContext(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public S? GetService<S>() where S: class
    {
        return (S?) _serviceProvider.GetService(typeof(S));
    }
}