using Proact.ActionFilter;

namespace Proact;

public static class FlashExtensions
{
    public static IServiceCollection AddFlash(this IServiceCollection services)
    {
        services.AddControllers(config => config.Filters.Add<FlashFilter>());
        services.AddSingleton<FlashService>();
        return services;
    }
}