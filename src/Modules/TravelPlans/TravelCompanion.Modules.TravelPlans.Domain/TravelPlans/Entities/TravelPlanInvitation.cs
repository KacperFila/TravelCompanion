using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

public sealed class TravelPlanInvitation
{
    public TravelPlanInvitationId Id { get; private set; }
    public AggregateId TravelPlanId { get; private set; }
    public ParticipantId ParticipantId { get; private set; }
    public TravelPlanInvitation(AggregateId travelPlanId, ParticipantId participantId)
    {
        Id = Guid.NewGuid();
        TravelPlanId = travelPlanId;
        ParticipantId = participantId;
    }

    public static TravelPlanInvitation Create(AggregateId travelPlanId, ParticipantId participantId)
    {
        var invitation = new TravelPlanInvitation(travelPlanId, participantId);
        
        return invitation;
    }
    
}