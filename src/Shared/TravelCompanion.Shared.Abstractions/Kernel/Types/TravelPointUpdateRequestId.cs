using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class TravelPointUpdateRequestId : TypeId
{
    public TravelPointUpdateRequestId(Guid value) : base(value)
    {
    }

    public static implicit operator TravelPointUpdateRequestId(Guid id) => new(id);
}