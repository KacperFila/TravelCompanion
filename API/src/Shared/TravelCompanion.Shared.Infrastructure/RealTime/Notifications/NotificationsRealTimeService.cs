using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Infrastructure.RealTime.Notifications.Hubs;

namespace TravelCompanion.Shared.Infrastructure.RealTime.Notifications;

internal class NotificationsRealTimeService : INotificationRealTimeService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationsRealTimeService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToAllAsync(INotificationMessage message)
    {
        //await _hubContext.Clients.All.ReceiveMessageAsync(message);
    }

    public async Task SendToAsync(string userId, INotificationMessage message)
    {
        //await _hubContext.Clients.User(userId).ReceiveMessageAsync(message);
    }
}