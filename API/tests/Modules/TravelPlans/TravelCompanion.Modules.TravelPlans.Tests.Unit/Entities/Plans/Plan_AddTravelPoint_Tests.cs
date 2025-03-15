using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;

public class Plan_AddTravelPoint_Tests
{
    private void Act(TravelPoint point) => _plan.AddTravelPoint(point);

    [Fact]
    public void given_travelPoint_does_not_exist_in_plan_addition_should_succeed()
    {
        var point = TravelPoint.Create(Guid.NewGuid(), "placeName", _plan.Id, false, 0);

        var exception = Record.Exception(() => Act(point));

        exception.ShouldBeNull();
        _plan.TravelPlanPoints.ShouldContain(point);
    }

    [Fact]
    public void given_travelPoint_is_already_added_addition_should_fail()
    {
        var point = TravelPoint.Create(Guid.NewGuid(), "placeName", _plan.Id, false, 0);
        _plan.AddTravelPoint(point);

        var exception = Record.Exception(() => Act(point));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TravelPointAlreadyAddedException>();
    }

    [Fact]
    public void given_travelPoint_is_null_addition_should_fail()
    {
        TravelPoint point = null;

        var exception = Record.Exception(() => Act(point));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidTravelPointException>();
    }

    private readonly Plan _plan;
    public Plan_AddTravelPoint_Tests()
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