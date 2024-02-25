using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class ParticipantId : TypeId
{
    public ParticipantId(Guid value) : base(value)
    {
    }

    protected ParticipantId() : base(Guid.NewGuid())
    {
    }

    public static implicit operator ParticipantId(Guid id) => new(id);
    public static implicit operator Guid(ParticipantId id) => id.Value;
}