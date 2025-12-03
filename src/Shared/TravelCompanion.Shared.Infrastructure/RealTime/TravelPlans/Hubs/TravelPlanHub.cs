using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;

[Authorize]
internal class TravelPlanHub : Hub<ITravelPlanHub>
{
    private readonly ConnectionManager _connectionManager;

    public TravelPlanHub(ConnectionManager connectionManager)
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