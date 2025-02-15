using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;

public class Plan_ChangeTo_Tests
{
    private void Act(DateOnly to) => _plan.ChangeTo(to);

    [Fact]
    public void given_to_is_later_than_from_change_should_succeed()
    {
        var to = new DateOnly(2030, 4, 10);

        var exception = Record.Exception(() => Act(to));

        exception.ShouldBeNull();
        _plan.To.ShouldBe(to);
    }

    [Fact]
    public void given_to_is_earlier_than_from_change_should_fail()
    {
        var to = new DateOnly(2030, 2, 10);

        var exception = Record.Exception(() => Act(to));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidPlanDateException>();
    }

    [Fact]
    public void given_to_equals_value_of_from_change_should_succeed()
    {
        var to = new DateOnly(2030, 3, 10);

        var exception = Record.Exception(() => Act(to));

        exception.ShouldBeNull();
        _plan.To.ShouldBe(to);
    }

    private readonly Plan _plan;
    public Plan_ChangeTo_Tests()
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