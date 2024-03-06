using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Invitations;

public class UserNotAllowedToManageInvitationException : TravelCompanionException
{
    public Guid Id { get; set; }
    public UserNotAllowedToManageInvitationException(Guid id) : base($"User with Id: {id} is not allowed to manage given invitation")
    {
        Id = id;
    }
}