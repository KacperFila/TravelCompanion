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
    private readonly ILogger<TravelPlansRealTimeService> _logger;

    public TravelPlansRealTimeService(
        IHubContext<TravelPlanHub, ITravelPlanHub> hubContext,
        ConnectionManager connectionManager
,
        ILogger<TravelPlansRealTimeService> logger)
    {
        _hubContext = hubContext;
        _connectionManager = connectionManager;
        _logger = logger;
    }

    public async Task SendRoadmapUpdate(List<string> participantUserIds, object plan)
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

    public async Task SendTravelPointUpdateRequestUpdate(List<string> participantUserIds, object updateRequests)
    {
        foreach (var userId in participantUserIds)
        {
            var connections = _connectionManager.GetConnections(userId);
            foreach (var connectionId in connections)
            {
                await _hubContext.Clients.Client(connectionId).ReceiveTravelPointUpdateRequestUpdate(updateRequests);
            }
        }
    }
}