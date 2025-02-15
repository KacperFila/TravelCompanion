using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;

public class Plan_RemoveTravelPoint_Tests
{
    private void Act(TravelPoint point) => _plan.RemoveTravelPoint(point);

    [Fact]
    public void given_point_is_added_to_plan_removal_should_succeed()
    {
        var point = TravelPoint.Create(Guid.NewGuid(), "placeName", _plan.Id, false);
        _plan.AddTravelPoint(point);

        var exception = Record.Exception(() => Act(point));

        exception.ShouldBeNull();
        _plan.TravelPlanPoints.ShouldNotContain(point);
    }

    [Fact]
    public void given_point_does_not_belong_to_plan_removal_should_fail()
    {
        var point = TravelPoint.Create(Guid.NewGuid(), "placeName", _plan.Id, false);

        var exception = Record.Exception(() => Act(point));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<TravelPointNotFoundException>();
    }

    [Fact]
    public void given_point_is_null_removal_should_fail()
    {
        TravelPoint point = null;

        var exception = Record.Exception(() => Act(point));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidTravelPointException>();
    }

    private readonly Plan _plan;
    public Plan_RemoveTravelPoint_Tests()
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