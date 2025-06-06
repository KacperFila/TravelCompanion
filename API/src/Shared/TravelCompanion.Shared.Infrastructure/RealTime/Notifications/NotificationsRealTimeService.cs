﻿using System;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Notifications;
using TravelCompanion.Shared.Abstractions.RealTime.Notifications;
using TravelCompanion.Shared.Infrastructure.RealTime.Notifications.Hubs;

namespace TravelCompanion.Shared.Infrastructure.RealTime.Notifications;

internal class NotificationsRealTimeService : INotificationRealTimeService
{
    private readonly IHubContext<NotificationHub, INotificationHub> _hubContext;
    private readonly ConnectionManager _connectionManager;

    public NotificationsRealTimeService(IHubContext<NotificationHub, INotificationHub> hubContext, ConnectionManager connectionManager)
    {
        _hubContext = hubContext;
        _connectionManager = connectionManager;
    }

    public async Task SendToAsync(Guid userId, INotificationMessage notification)
    {
        var connections = _connectionManager
            .GetConnections(userId.ToString())
            .Distinct();

        foreach (var connectionId in connections)
        {
            await _hubContext.Clients.Client(connectionId)
                .ReceiveNotification(notification);
        }
    }

    public async Task SendToGroup(List<Guid> usersIds, INotificationMessage notification)
    {
        var sentConnections = new HashSet<string>();

        foreach (var userId in usersIds)
        {
            var connections = _connectionManager.GetConnections(userId.ToString());

            foreach (var connectionId in connections)
            {
                if (sentConnections.Add(connectionId))
                {
                    await _hubContext.Clients.Client(connectionId).ReceiveNotification(notification);
                }
            }
        }
    }
}
