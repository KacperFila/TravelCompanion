using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class InvitationId : TypeId
{
    public InvitationId(Guid value) : base(value)
    {
    }

    protected InvitationId() : base(Guid.NewGuid())
    {
    }

    public static implicit operator InvitationId(Guid id) => new(id);
    public static implicit operator Guid(InvitationId id) => id.Value;
}