using System;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Users.Core.Events.External;

public record ActiveTravelChanged(Guid UserId, Guid TravelId) : IEvent;