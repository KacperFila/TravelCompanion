using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelCompanion.Modules.RealtimeNotifications.Core;
using TravelCompanion.Shared.Abstractions.Modules;
using TravelCompanion.Shared.Infrastructure.Notifications;

namespace TravelCompanion.Modules.RealtimeNotifications.Api;

internal class RealtimeNotificationsModule : IModule
{
    public const string BasePath = "realtimenotifications-module";
    public string Name { get; } = "RealtimeNotifications";
    public string Path => BasePath;
    public void Register(IServiceCollection services)
    {
        services
            .AddCore();
    }

    public void Use(IApplicationBuilder app)
    {
        app.UseEndpoints(
            x => x.MapHub<NotificationsHub>("notifications"));
    }
}