using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Notifications;

public interface INotificationService
{
    public Task SendToAllAsync(string message);
    public Task SendToAsync(string userId, string message);
}