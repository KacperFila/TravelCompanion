using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TravelCompanion.Shared.Infrastructure.RealTime.Travels.Hubs;

[Authorize]
internal class TravelHub : Hub<ITravelHub>
{
    private readonly ConnectionManager _connectionManager;

    public TravelHub(ConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Context.User.Identity.Name;
        if (!string.IsNullOrEmpty(userId))
        {
            _connectionManager.AddConnection(userId, Context.ConnectionId);
        }

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.UserIdentifier;
        if (!string.IsNullOrEmpty(userId))
        {
            _connectionManager.RemoveConnection(userId, Context.ConnectionId);
        }

        await base.OnDisconnectedAsync(exception);
    }
}