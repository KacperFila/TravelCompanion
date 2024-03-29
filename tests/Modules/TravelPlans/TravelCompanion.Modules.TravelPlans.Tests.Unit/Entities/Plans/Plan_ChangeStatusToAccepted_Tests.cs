using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;

public class Plan_ChangeStatusToAccepted_Tests
{
    private void Act() => _plan.ChangeStatusToAccepted();

    [Fact]
    public void given_plan_is_during_acceptance_status_change_to_accepted_should_succeed()
    {
        _plan.ChangeStatusToDuringAcceptance();

        var exception = Record.Exception(Act);

        exception.ShouldBeNull();
        _plan.PlanStatus.ShouldBe(PlanStatus.Accepted);
    }

    [Fact]
    public void given_plan_is_during_planning_status_change_to_accepted_should_succeed()
    {
        // during planning by default

        var exception = Record.Exception(Act);

        exception.ShouldBeNull();
        _plan.PlanStatus.ShouldBe(PlanStatus.Accepted);
    }

    [Fact]
    public void given_plan_already_accepted_status_change_to_accepted_should_fail()
    {
        _plan.ChangeStatusToAccepted();

        var exception = Record.Exception(Act);

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PlanAlreadyAcceptedException>();
    }


    private readonly Plan _plan;
    public Plan_ChangeStatusToAccepted_Tests()
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