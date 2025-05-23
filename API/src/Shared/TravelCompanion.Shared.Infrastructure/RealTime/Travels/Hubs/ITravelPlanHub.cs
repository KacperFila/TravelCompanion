using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.RealTime.Travels.Hubs;

public interface ITravelHub
{
    Task ReceiveTravelUpdate(object travel);
    Task ReceiveActiveTravelChanged(string travelId);
}
