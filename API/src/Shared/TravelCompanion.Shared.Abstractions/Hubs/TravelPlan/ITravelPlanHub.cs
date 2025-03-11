using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.Hubs.TravelPlan;

// METHODS TO BE USED BY CLIENT
public interface ITravelPlanHub
{
    Task ReceivePlanUpdate(object plan);
}
