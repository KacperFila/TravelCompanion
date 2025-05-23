using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TravelCompanion.Shared.Abstractions.RealTime.Travels;
using TravelCompanion.Shared.Infrastructure.RealTime.Travels.Hubs;

namespace TravelCompanion.Shared.Infrastructure.RealTime.Travels;

internal class TravelsRealTimeService : ITravelsRealTimeService
{
    private readonly IHubContext<TravelHub, ITravelHub> _hubContext;
    private readonly ConnectionManager _connectionManager;

    public TravelsRealTimeService(
        IHubContext<TravelHub, ITravelHub> hubContext,
        ConnectionManager connectionManager)
    {
        _hubContext = hubContext;
        _connectionManager = connectionManager;
    }

    public async Task SendTravelUpdate(List<Guid> participantUserIds, object travel)
    {
        foreach (var userId in participantUserIds)
        {
            var connections = _connectionManager.GetConnections(userId.ToString());
            foreach (var connectionId in connections)
            {
                await _hubContext.Clients.Client(connectionId).ReceiveTravelUpdate(travel);
            }
        }
    }

    public async Task SendActiveTravelChanged(Guid userId, Guid travelId)
    {
        var connections = _connectionManager.GetConnections(userId.ToString());
        foreach (var connectionId in connections)
        {
            await _hubContext.Clients.Client(connectionId).ReceiveActiveTravelChanged(travelId.ToString());
        }
    }
}