using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Notifications;

namespace TravelCompanion.Shared.Abstractions.RealTime.Notifications;

public interface INotificationRealTimeService
{
    public Task SendToGroup(List<Guid> usersIds, INotificationMessage message);
    public Task SendToAsync(Guid userId, INotificationMessage message);
}