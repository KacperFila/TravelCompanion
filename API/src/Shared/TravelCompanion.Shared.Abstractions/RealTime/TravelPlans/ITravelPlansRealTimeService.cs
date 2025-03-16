using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

public interface ITravelPlansRealTimeService
{
    Task SendRoadmapUpdate(List<string> participantUserIds, object plan);
    Task SendTravelPointUpdateRequestUpdate(List<string> participantUserIds, object plan);
}
