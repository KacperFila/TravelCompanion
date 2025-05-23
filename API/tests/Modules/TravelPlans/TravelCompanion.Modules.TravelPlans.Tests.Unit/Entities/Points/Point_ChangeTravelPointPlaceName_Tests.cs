using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Points;

public class PointChangeTravelPointPlaceNameTests
{
    private void Act(string placeName) => _point.ChangeTravelPointPlaceName(placeName);

    [Fact]
    public void given_point_place_name_is_correct_change_should_succeed()
    {
        var placeName = "new placeName";

        var exception = Record.Exception(() => Act(placeName));

        exception.ShouldBeNull();
        _point.PlaceName.ShouldBe(placeName);
    }

    [Fact]
    public void given_point_place_name_is_empty_change_should_fail()
    {
        var placeName = string.Empty;

        var exception = Record.Exception(() => Act(placeName));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyTravelPointPlaceNameException>();
    }

    private readonly TravelPoint _point;
    public PointChangeTravelPointPlaceNameTests()
    {
        _point = TravelPoint.Create(
            Guid.NewGuid(),
            "placeName",
            Guid.NewGuid(),
            false,
            0);
    }
}