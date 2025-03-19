using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;
using TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;

namespace TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans;

internal class TravelPlansRealTimeService : ITravelPlansRealTimeService
{
    private readonly IHubContext<TravelPlanHub, ITravelPlanHub> _hubContext;
    private readonly ConnectionManager _connectionManager;

    public TravelPlansRealTimeService(
        IHubContext<TravelPlanHub, ITravelPlanHub> hubContext,
        ConnectionManager connectionManager)
    {
        _hubContext = hubContext;
        _connectionManager = connectionManager;
    }

    public async Task SendPlanUpdate(List<string> participantUserIds, object plan)
    {
        foreach (var userId in participantUserIds)
        {
            var connections = _connectionManager.GetConnections(userId);
            foreach (var connectionId in connections)
            {
                await _hubContext.Clients.Client(connectionId).ReceivePlanUpdate(plan);
            }
        }
    }

    public async Task SendPointUpdateRequestUpdate(List<string> participantUserIds, object payload)
    {
        foreach (var userId in participantUserIds)
        {
            var connections = _connectionManager.GetConnections(userId);
            foreach (var connectionId in connections)
            {
                await _hubContext.Clients.Client(connectionId).ReceiveTravelPointUpdateRequestUpdate(payload);
            }
        }
    }
}