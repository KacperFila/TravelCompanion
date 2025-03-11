using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Notifications;

public interface INotificationRealTimeService
{
    public Task SendToAllAsync(INotificationMessage message);
    public Task SendToAsync(string userId, INotificationMessage message);
}