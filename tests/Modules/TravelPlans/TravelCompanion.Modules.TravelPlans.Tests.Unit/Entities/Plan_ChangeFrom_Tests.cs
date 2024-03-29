using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities;

public class Plan_ChangeFrom_Tests
{
    private void Act(DateOnly from) => _plan.ChangeFrom(from);

    [Fact]
    public void given_from_is_earlier_than_to_change_should_succeed()
    {
        var from = new DateOnly(2030, 1, 10);

        var exception = Record.Exception(() => Act(from));

        exception.ShouldBeNull();
        _plan.From.ShouldBe(from);
    }

    [Fact]
    public void given_from_is_later_than_to_change_should_fail()
    {
        var from = new DateOnly(2030, 4, 10);

        var exception = Record.Exception(() => Act(from));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidPlanDateException>();
    }

    [Fact]
    public void given_from_equals_value_of_to_change_should_succeed()
    {
        var from = new DateOnly(2030, 3, 15);

        var exception = Record.Exception(() => Act(from));

        exception.ShouldBeNull();
        _plan.From.ShouldBe(from);
    }


    private readonly Plan _plan;
    public Plan_ChangeFrom_Tests()
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