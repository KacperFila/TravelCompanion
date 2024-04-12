using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.Notifications;

public interface INotificationClient
{
    Task ReceiveMessageAsync(string message);
}