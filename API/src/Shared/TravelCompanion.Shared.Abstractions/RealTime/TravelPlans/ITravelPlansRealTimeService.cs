using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

public interface ITravelPlansRealTimeService
{
    Task SendPlanUpdate(List<string> participantUserIds, object plan);
    Task SendPointUpdateRequestUpdate(List<string> participantUserIds, object payload);
}
