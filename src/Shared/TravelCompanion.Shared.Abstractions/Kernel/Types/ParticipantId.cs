using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class ParticipantId : TypeId
{
    public ParticipantId(Guid value) : base(value)
    {
    }

    public static implicit operator ParticipantId(Guid id) => new (id);
}