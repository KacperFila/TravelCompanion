using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Notifications;

namespace TravelCompanion.Shared.Infrastructure.Notifications;

public class NotificationsService : INotificationService
{
    private readonly IHubContext<NotificationsHub, INotificationClient> _hubContext;

    public NotificationsService(IHubContext<NotificationsHub, INotificationClient> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendToAllAsync(INotificationMessage message)
    {
        await _hubContext.Clients.All.ReceiveMessageAsync(message);
    }

    public async Task SendToAsync(string userId, INotificationMessage message)
    {
        await _hubContext.Clients.User(userId).ReceiveMessageAsync(message);
    }
}