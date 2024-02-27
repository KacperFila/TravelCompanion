using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlanInvitations.Commands.Handlers;

internal sealed class AcceptInvitationToTravelPlanHandler : ICommandHandler<AcceptInvitationToTravelPlan>
{
    private readonly ITravelPlanRepository _travelPlanRepository;
    private readonly ITravelPlanInvitationRepository _travelPlanInvitationRepository;

    public AcceptInvitationToTravelPlanHandler(ITravelPlanRepository travelPlanRepository, ITravelPlanInvitationRepository travelPlanInvitationRepository)
    {
        _travelPlanRepository = travelPlanRepository;
        _travelPlanInvitationRepository = travelPlanInvitationRepository;
    }

    public async Task HandleAsync(AcceptInvitationToTravelPlan command)
    {
        var invitation = await _travelPlanInvitationRepository.GetAsync(command.invitationId);

        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.invitationId);
        }
        
        var travelPlan = await _travelPlanRepository.GetAsync(invitation.TravelPlanId);

        if (travelPlan is null)
        {
            throw new TravelPlanNotFoundException(invitation.TravelPlanId);
        }

        await _travelPlanInvitationRepository.RemoveInvitationAsync(command.invitationId);
        
        travelPlan.AddParticipant(invitation.ParticipantId);
        await _travelPlanRepository.UpdateAsync(travelPlan);

        //TODO Remove migration with isAccepted bool, add authorization and check for duplicates and user not inviting himself
    }
}