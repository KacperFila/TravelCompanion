using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Invitation
{
    public InvitationId Id { get; private set; }
    public AggregateId TravelPlanId { get; private set; }
    public ParticipantId ParticipantId { get; private set; }
    public Invitation(AggregateId travelPlanId, ParticipantId participantId)
    {
        Id = Guid.NewGuid();
        TravelPlanId = travelPlanId;
        ParticipantId = participantId;
    }

    public static Invitation Create(AggregateId travelPlanId, ParticipantId participantId)
    {
        var invitation = new Invitation(travelPlanId, participantId);
        
        return invitation;
    }
    
}