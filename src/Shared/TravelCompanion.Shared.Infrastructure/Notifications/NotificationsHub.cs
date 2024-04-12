using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.Notifications;

public sealed class NotificationsHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveMessageAsync($"{Context.ConnectionId} has joined.");
    }

}