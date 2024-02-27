using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlanInvitations.Commands.Handlers;

internal sealed class RejectInvitationToTravelPlanHandler : ICommandHandler<RejectInvitationToTravelPlan>
{
    private readonly ITravelPlanInvitationRepository _travelPlanInvitationRepository;

    public RejectInvitationToTravelPlanHandler(ITravelPlanInvitationRepository travelPlanInvitationRepository)
    {
        _travelPlanInvitationRepository = travelPlanInvitationRepository;
    }

    public async Task HandleAsync(RejectInvitationToTravelPlan command)
    {
        var doesInvitationExist = await _travelPlanInvitationRepository.ExistsByIdAsync(command.invitationId);

        if (!doesInvitationExist)
        {
            throw new InvitationNotFoundException(command.invitationId);
        }

        await _travelPlanInvitationRepository.RemoveInvitationAsync(command.invitationId);
    }
}