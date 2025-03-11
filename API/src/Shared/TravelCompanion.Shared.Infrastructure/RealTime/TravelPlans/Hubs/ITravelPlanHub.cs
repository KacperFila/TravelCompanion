using System.Threading.Tasks;

namespace TravelCompanion.Shared.Infrastructure.RealTime.TravelPlans.Hubs;

// METHODS TO BE USED BY CLIENT
public interface ITravelPlanHub
{
    Task ReceivePlanUpdate(object plan);
}
