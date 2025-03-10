using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.TravelPlans.Api.Hubs;

[assembly:InternalsVisibleTo("TravelCompanion.Modules.TravelPlans.Tests.Unit")]
[assembly:InternalsVisibleTo("DynamicProxyAssemblyGen2")]
namespace TravelCompanion.Modules.TravelPlans.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }

    public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<TravelPlanHub>("travelPlanHub");
        });

        return app;
    }
}