using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

public interface ITravelPlansRealTimeService
{
    Task SendPlanUpdate(List<string> participantUserIds, object plan);
    Task SendPointUpdateRequestUpdate(List<string> participantUserIds, object updateRequests);
    Task SendPlanInvitation(string inviteeId, object invitation);
    Task SendPlanInvitationRemoved(string inviteeId, object payload);
    Task SendActivePlanChanged(string userId, string planId);
}
