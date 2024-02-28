using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class RejectInvitationToTravelPlanHandler : ICommandHandler<RejectInvitation>
{
    private readonly IInvitationRepository _invitationRepository;

    public RejectInvitationToTravelPlanHandler(IInvitationRepository invitationRepository)
    {
        _invitationRepository = invitationRepository;
    }

    public async Task HandleAsync(RejectInvitation command)
    {
        var doesInvitationExist = await _invitationRepository.ExistsByIdAsync(command.invitationId);

        if (!doesInvitationExist)
        {
            throw new InvitationNotFoundException(command.invitationId);
        }

        await _invitationRepository.RemoveAsync(command.invitationId);
    }
}