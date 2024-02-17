using System;
using TravelCompanion.Shared.Abstractions.Time;

namespace TravelCompanion.Shared.Infrastructure.Time
{
    internal class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
    }
}