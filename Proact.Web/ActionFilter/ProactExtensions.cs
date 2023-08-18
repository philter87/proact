using Proact.Core;

namespace Proact.ActionFilter;

public static class ProactExtensions
{
    public static IServiceCollection AddProact(this IServiceCollection services)
    {
        services.AddControllers(config => config.Filters.Add<ProactActionFilter>());
        services.AddSingleton<ProactService>();
        return services;
    }
}