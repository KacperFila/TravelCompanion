﻿using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;

public class TravelPointAlreadyAddedException : TravelCompanionException
{
    public Guid pointId { get; set; }
    public TravelPointAlreadyAddedException(Guid pointId) : base($"Point with Id: {pointId} is already added to plan.")
    {
        pointId = pointId;
    }
}