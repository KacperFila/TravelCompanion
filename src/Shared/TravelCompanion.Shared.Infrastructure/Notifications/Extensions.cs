using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Shared.Abstractions.Notifications;

namespace TravelCompanion.Shared.Infrastructure.Notifications;

internal static class Extensions
{
    public static IServiceCollection AddNotifications(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddTransient<INotificationService, NotificationsService>();
        services.AddTransient<NotificationsHub>();

        return services;
    }
}