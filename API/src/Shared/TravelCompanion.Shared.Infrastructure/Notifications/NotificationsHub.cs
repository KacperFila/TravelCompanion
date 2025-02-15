using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace TravelCompanion.Shared.Infrastructure.Notifications;

[Authorize]
public sealed class NotificationsHub : Hub<INotificationClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveMessageAsync(NotificationMessage.Create("Connected!", $"ConnectionId: {Context.ConnectionId}"));
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await Clients.All.ReceiveMessageAsync(NotificationMessage.Create("Disconnected!", $"ConnectionId: {Context.ConnectionId}"));
    }
}