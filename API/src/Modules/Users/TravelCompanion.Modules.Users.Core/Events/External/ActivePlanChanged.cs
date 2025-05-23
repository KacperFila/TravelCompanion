using System;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Users.Core.Events.External;

public record ActivePlanChanged(Guid UserId, Guid PlanId) : IEvent;