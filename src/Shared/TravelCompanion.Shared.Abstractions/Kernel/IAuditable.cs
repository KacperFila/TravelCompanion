using System;

namespace TravelCompanion.Shared.Abstractions.Kernel;

public interface IAuditable
{
    DateTime CreatedOnUtc { get; set; }
    DateTime? ModifiedOnUtc { get; set; }
}