using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Invitation
{
    public InvitationId Id { get; private set; }
    public AggregateId TravelPlanId { get; private set; }
    public EntityId InviteeId { get; private set; }
    public Invitation(AggregateId travelPlanId, EntityId inviteeId)
    {
        Id = Guid.NewGuid();
        TravelPlanId = travelPlanId;
        InviteeId = inviteeId;
    }

    public static Invitation Create(AggregateId travelPlanId, EntityId inviteeId)
    {
        var invitation = new Invitation(travelPlanId, inviteeId);
        
        return invitation;
    }
    
}