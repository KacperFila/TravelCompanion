using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class OwnerId : TypeId
{
    public OwnerId(Guid value) : base(value)
    {
    }

    public static implicit operator OwnerId(Guid id) => new (id);
}