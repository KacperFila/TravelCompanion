using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Invitations;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
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
        
        var plan = await _planRepository.GetAsync(invitation.TravelPlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(invitation.TravelPlanId);
        }

        if (plan.PlanStatus != PlanStatus.DuringPlanning)
        {
            throw new PlanNotDuringPlanningException(plan.Id);
        }

        await _invitationRepository.RemoveAsync(command.invitationId);
        
        plan.AddParticipant(invitation.InviteeId);
        await _planRepository.UpdateAsync(plan);
    }
}