using System;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Users.Core.Events.External;

public record PlanCreated(Guid OwnerId, Guid PlanId) : IEvent;