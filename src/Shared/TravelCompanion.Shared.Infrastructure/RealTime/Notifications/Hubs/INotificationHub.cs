using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.RealTime.Notifications.Hubs;

public interface INotificationHub
{
    Task ReceiveNotification(object notification);
}
