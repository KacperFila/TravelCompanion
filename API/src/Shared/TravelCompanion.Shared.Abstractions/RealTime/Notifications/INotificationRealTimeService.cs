using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Notifications;

public interface INotificationRealTimeService
{
    public Task SendToGroup(List<string> usersIds, INotificationMessage message);
    public Task SendToAsync(string userId, INotificationMessage message);
}