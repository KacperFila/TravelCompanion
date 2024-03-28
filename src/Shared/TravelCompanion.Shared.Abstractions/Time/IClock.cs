using System;

namespace TravelCompanion.Shared.Abstractions.Time
{
    public interface IClock
    {
        DateTime CurrentDate();
        DateOnly CurrentDateOnly();
        DateTime AddDays(DateTime date, int daysToAdd);
        DateOnly AddDateOnlyDays(DateOnly date, int daysToAdd);
    }
}