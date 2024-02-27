using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class TravelPointChangeSuggestionId : TypeId
{
    public TravelPointChangeSuggestionId(Guid value) : base(value)
    {
    }

    public static implicit operator TravelPointChangeSuggestionId(Guid id) => new(id);
}