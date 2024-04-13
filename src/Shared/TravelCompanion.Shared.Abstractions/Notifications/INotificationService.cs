using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Notifications;

public interface INotificationService
{
    public Task SendToAllAsync(INotificationMessage message);
    public Task SendToAsync(string userId, INotificationMessage message);
}