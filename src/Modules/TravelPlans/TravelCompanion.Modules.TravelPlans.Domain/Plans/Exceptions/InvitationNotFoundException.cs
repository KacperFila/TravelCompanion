using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;

public class InvitationNotFoundException : TravelCompanionException
{
    public Guid Id { get; set; }
    public InvitationNotFoundException(Guid id) : base($"Invitation with Id: {id} was not found.")
    {
        Id = id;
    }
}