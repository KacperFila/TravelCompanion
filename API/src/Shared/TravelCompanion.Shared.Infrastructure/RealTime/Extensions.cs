using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;
using TravelCompanion.Shared.Abstractions.RealTime.Travels;
using TravelCompanion.Shared.Infrastructure.RealTime.Notifications;
using TravelCompanion.Shared.Infrastructure.RealTime.Notifications.Hubs;
using TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans;
using TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;
using TravelCompanion.Shared.Infrastructure.RealTime.Travels;
using TravelCompanion.Shared.Infrastructure.RealTime.Travels.Hubs;

namespace TravelCompanion.Shared.Infrastructure.RealTime;

internal static class Extensions
{
    public static IServiceCollection AddRealTimeCommunication(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<INotificationRealTimeService, NotificationsRealTimeService>();
        services.AddScoped<ITravelPlansRealTimeService, TravelPlansRealTimeService>();
        services.AddScoped<ITravelsRealTimeService, TravelsRealTimeService>();
        services.AddSingleton<ConnectionManager>();

        return services;
    }

    public static IApplicationBuilder UseRealTimeCommunication(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<TravelPlanHub>("travelPlanHub");
            endpoints.MapHub<TravelHub>("travelHub");
            endpoints.MapHub<NotificationHub>("notifications");
        });

        return app;
    }
}