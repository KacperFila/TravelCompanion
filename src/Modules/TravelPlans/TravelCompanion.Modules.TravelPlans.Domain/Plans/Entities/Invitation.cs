using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class Invitation : IAuditable
{
    public InvitationId Id { get; private set; }
    public AggregateId TravelPlanId { get; private set; }
    public EntityId InviteeId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Invitation(AggregateId travelPlanId, EntityId inviteeId)
    {
        Id = Guid.NewGuid();
        TravelPlanId = travelPlanId;
        InviteeId = inviteeId;
    }

    public static Invitation Create(AggregateId travelPlanId, EntityId inviteeId)
    => new(travelPlanId, inviteeId);
}