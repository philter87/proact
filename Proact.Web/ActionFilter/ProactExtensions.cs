using Proact.Core;
using Proact.Web.ActionFilter;

namespace Proact.ActionFilter;

public static class ProactExtensions
{
    public static IServiceCollection AddProact(this IServiceCollection services)
    {
        services.AddControllers(config => config.Filters.Add<ProactActionFilter>());
        services.AddSingleton<ProactService>();
        return services;
    }

    public static WebApplication UseProact(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseWebSockets();
        }

        return app;
    }
}