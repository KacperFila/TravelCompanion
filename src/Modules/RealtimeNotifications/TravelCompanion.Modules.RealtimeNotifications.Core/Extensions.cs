using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TravelCompanion.Modules.RealtimeNotifications.Api")]
namespace TravelCompanion.Modules.RealtimeNotifications.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSignalR();
        return services;
    }
}