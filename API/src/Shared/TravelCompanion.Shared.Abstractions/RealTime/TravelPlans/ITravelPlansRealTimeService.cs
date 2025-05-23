using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

public interface ITravelPlansRealTimeService
{
    Task SendPlanUpdate(List<Guid> participantUserIds, object plan);
    Task SendPointUpdateRequestUpdate(List<Guid> participantUserIds, object updateRequests);
    Task SendPlanInvitation(Guid inviteeId, object invitation);
    Task SendPlanInvitationRemoved(Guid inviteeId, object payload);
    Task SendActivePlanChanged(Guid userId, Guid planId);
}
