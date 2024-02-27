using System;

namespace TravelCompanion.Shared.Abstractions.Kernel.Types;

public class TravelPlanInvitationId : TypeId
{
    public TravelPlanInvitationId(Guid value) : base(value)
    {
    }

    protected TravelPlanInvitationId() : base(Guid.NewGuid())
    {
    }

    public static implicit operator TravelPlanInvitationId(Guid id) => new(id);
    public static implicit operator Guid(TravelPlanInvitationId id) => id.Value;
}