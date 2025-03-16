using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;

public class Plan_AddParticipant_Tests
{
    private void Act(Guid participantId)
    {
        var planParticipantRecord = PlanParticipantRecord.Create(participantId, _plan.Id);
        _plan.AddParticipant(planParticipantRecord);
    }

    [Fact]
    public void given_participant_does_not_participate_in_plan_yet_addition_should_succeed()
    {
        var participantId = new EntityId(Guid.NewGuid());

        var exception = Record.Exception(() => Act(participantId));

        exception.ShouldBeNull();
        _plan.Participants.Select(x => x.ParticipantId).ShouldContain(participantId);
    }

    [Fact]
    public void given_participant_already_participates_in_plan_addition_should_fail()
    {
        var participantId = new EntityId(Guid.NewGuid());

        var planParticipantRecord = PlanParticipantRecord.Create(participantId, _plan.Id);
        _plan.AddParticipant(planParticipantRecord);

        var exception = Record.Exception(() => Act(participantId));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<UserAlreadyParticipatesInPlanException>();
    }

    private readonly Plan _plan;
    public Plan_AddParticipant_Tests()
    {
        _plan = new Plan(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "title",
            "desc",
            new DateOnly(2030, 3, 10),
            new DateOnly(2030, 3, 15));
    }
}