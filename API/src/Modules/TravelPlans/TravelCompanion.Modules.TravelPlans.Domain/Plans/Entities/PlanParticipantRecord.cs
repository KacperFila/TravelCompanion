using TravelCompanion.Shared.Abstractions.Kernel;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class PlanParticipantRecord : IAuditable
{
    public Guid Id { get; private set; }
    public Guid ParticipantId { get; private set; }
    public AggregateId? PlanId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static PlanParticipantRecord Create(Guid participantId, Guid travelPlanId)
    {
        var participantRecord = new PlanParticipantRecord();
        participantRecord.Id = Guid.NewGuid();
        participantRecord.ChangeParticipantId(participantId);
        participantRecord.ChangeTravelPlanId(travelPlanId);

        return participantRecord;
    }

    public void ChangeParticipantId(Guid participantId)
    {
        ParticipantId = participantId;
    }

    public void ChangeTravelPlanId(Guid travelPlanId)
    {
        PlanId = travelPlanId;
    }
}