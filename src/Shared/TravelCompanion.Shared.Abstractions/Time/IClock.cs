using System;

namespace TravelCompanion.Shared.Abstractions.Time
{
    public interface IClock
    {
        DateTime CurrentDate();
    }
}