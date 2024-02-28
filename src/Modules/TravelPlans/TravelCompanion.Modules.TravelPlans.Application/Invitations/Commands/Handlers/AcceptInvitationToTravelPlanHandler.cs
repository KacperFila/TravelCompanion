using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class AcceptInvitationToTravelPlanHandler : ICommandHandler<AcceptInvitation>
{
    private readonly ITravelPlanRepository _travelPlanRepository;
    private readonly IInvitationRepository _invitationRepository;

    public AcceptInvitationToTravelPlanHandler(ITravelPlanRepository travelPlanRepository, IInvitationRepository invitationRepository)
    {
        _travelPlanRepository = travelPlanRepository;
        _invitationRepository = invitationRepository;
    }

    public async Task HandleAsync(AcceptInvitation command)
    {
        var invitation = await _invitationRepository.GetAsync(command.invitationId);

        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.invitationId);
        }
        
        var travelPlan = await _travelPlanRepository.GetAsync(invitation.TravelPlanId);

        if (travelPlan is null)
        {
            throw new TravelPlanNotFoundException(invitation.TravelPlanId);
        }

        await _invitationRepository.RemoveAsync(command.invitationId);
        
        travelPlan.AddParticipant(invitation.ParticipantId);
        await _travelPlanRepository.UpdateAsync(travelPlan);

        //TODO Remove migration with isAccepted bool, add authorization and check for duplicates and user not inviting himself
    }
}