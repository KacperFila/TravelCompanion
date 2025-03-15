using TravelCompanion.Shared.Abstractions.Kernel;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

public sealed class PlanParticipantRecord : IAuditable
{
    public Guid Id { get; private set; }
    public Guid ParticipantId { get; private set; }
    public Guid TravelPlanId { get; private set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    private PlanParticipantRecord()
    {
        Id = Guid.NewGuid();
    }

    public static PlanParticipantRecord Create(Guid participantId, Guid travelPlanId)
    {
        var participantRecord = new PlanParticipantRecord();
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
        TravelPlanId = travelPlanId;
    }
}
