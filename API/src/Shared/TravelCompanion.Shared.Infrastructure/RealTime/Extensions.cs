using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;
using TravelCompanion.Shared.Infrastructure.RealTime.Notifications;
using TravelCompanion.Shared.Infrastructure.RealTime.Notifications.Hubs;
using TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans;
using TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;

namespace TravelCompanion.Shared.Infrastructure.Notifications;

internal static class Extensions
{
    public static IServiceCollection AddRealTimeCommunication(this IServiceCollection services)
    {
        services.AddSignalR(); ;
        services.AddScoped<INotificationRealTimeService, NotificationsRealTimeService>();
        services.AddScoped<ITravelPlansRealTimeService, TravelPlansRealTimeService>();
        services.AddSingleton<ConnectionManager>();

        return services;
    }

    public static IApplicationBuilder UseRealTimeCommunication(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<TravelPlanHub>("/travelPlanHub");
            endpoints.MapHub<NotificationHub>("/notifications");
        });

        return app;
    }
}