using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class AcceptInvitationToTravelPlanHandler : ICommandHandler<AcceptInvitation>
{
    private readonly IPlanRepository _planRepository;
    private readonly IInvitationRepository _invitationRepository;

    public AcceptInvitationToTravelPlanHandler(IPlanRepository planRepository, IInvitationRepository invitationRepository)
    {
        _planRepository = planRepository;
        _invitationRepository = invitationRepository;
    }

    public async Task HandleAsync(AcceptInvitation command)
    {
        var invitation = await _invitationRepository.GetAsync(command.invitationId);

        if (invitation is null)
        {
            throw new InvitationNotFoundException(command.invitationId);
        }
        
        var travelPlan = await _planRepository.GetAsync(invitation.TravelPlanId);

        if (travelPlan is null)
        {
            throw new PlanNotFoundException(invitation.TravelPlanId);
        }

        await _invitationRepository.RemoveAsync(command.invitationId);
        
        travelPlan.AddParticipant(invitation.ParticipantId);
        await _planRepository.UpdateAsync(travelPlan);

        //TODO Remove migration with isAccepted bool, add authorization and check for duplicates and user not inviting himself
    }
}