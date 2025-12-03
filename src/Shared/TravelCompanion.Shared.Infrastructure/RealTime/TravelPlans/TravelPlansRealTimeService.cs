using System;
using Microsoft.AspNetCore.SignalR;
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

    public async Task SendActivePlanChanged(Guid userId, Guid planId)
    {
        var connections = _connectionManager.GetConnections(userId.ToString());
        foreach (var connectionId in connections)
        {
            await _hubContext.Clients.Client(connectionId).ReceiveActivePlanChanged(planId.ToString());
        }
    }

    public async Task SendPlanInvitation(Guid inviteeId, object invitation)
    {
        var connections = _connectionManager.GetConnections(inviteeId.ToString());
        foreach (var connectionId in connections)
        {
            await _hubContext.Clients.Client(connectionId).ReceivePlanInvitation(invitation);
        }
    }

    public async Task SendPlanInvitationRemoved(Guid inviteeId, object payload)
    {
        var connections = _connectionManager.GetConnections(inviteeId.ToString());
        foreach (var connectionId in connections)
        {
            await _hubContext.Clients.Client(connectionId).ReceivePlanInvitationRemoved(payload);
        }
    }

    public async Task SendPlanUpdate(List<Guid> participantUserIds, object plan)
    {
        foreach (var userId in participantUserIds)
        {
            var connections = _connectionManager.GetConnections(userId.ToString());
            foreach (var connectionId in connections)
            {
                await _hubContext.Clients.Client(connectionId).ReceivePlanUpdate(plan);
            }
        }
    }

    public async Task SendPointUpdateRequestUpdate(List<Guid> participantUserIds, object payload)
    {
        foreach (var userId in participantUserIds)
        {
            var connections = _connectionManager.GetConnections(userId.ToString());
            foreach (var connectionId in connections)
            {
                await _hubContext.Clients.Client(connectionId).ReceiveTravelPointUpdateRequestUpdate(payload);
            }
        }
    }
}