using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class TravelPointId : TypeId
{
    public TravelPointId(Guid value) : base(value)
    {
    }

    public static implicit operator TravelPointId(Guid id) => new(id);
}