﻿using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Points;

public class PointAcceptTravelPointTests
{
    private void Act() => _point.AcceptTravelPoint();

    [Fact]
    public void given_point_is_not_accepted_acceptance_should_succeed()
    {
        var exception = Record.Exception(Act);

        exception.ShouldBeNull();
        _point.IsAccepted.ShouldBeTrue();
    }

    [Fact]
    public void given_point_is_already_accepted_acceptance_should_fail()
    {
        _point.AcceptTravelPoint();

        var exception = Record.Exception(_point.AcceptTravelPoint);

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<PointAlreadyAcceptedException>();
    }

    private readonly TravelPoint _point;
    public PointAcceptTravelPointTests()
    {
        _point = TravelPoint.Create(
            Guid.NewGuid(),
            "placeName",
            Guid.NewGuid(),
            false,
            0);
    }
}