using System;
using TravelCompanion.Shared.Abstractions.Time;

namespace TravelCompanion.Shared.Infrastructure.Time
{
    internal class UtcClock : IClock
    {
        public DateTime CurrentDate() => DateTime.UtcNow;
        public DateOnly CurrentDateOnly() => DateOnly.FromDateTime(DateTime.UtcNow);
        public DateTime AddDays(DateTime date, int daysToAdd) => date.AddDays(daysToAdd);
        public DateOnly AddDateOnlyDays(DateOnly date, int daysToAdd) => date.AddDays(daysToAdd);
    }
}