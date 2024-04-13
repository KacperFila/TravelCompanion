using System.Threading.Tasks;
using TravelCompanion.Shared.Abstractions.Notifications;

namespace TravelCompanion.Shared.Infrastructure.Notifications;

public interface INotificationClient
{
    Task ReceiveMessageAsync(INotificationMessage message);
}