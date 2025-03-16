using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;

public interface ITravelPlanHub
{
    Task ReceivePlanUpdate(object plan);
    Task ReceiveTravelPointUpdateRequestUpdate(object request);
}
