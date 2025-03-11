using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

public interface ITravelPlansRealTimeService
{
    Task SendRoadmapUpdate(object plan);
}
