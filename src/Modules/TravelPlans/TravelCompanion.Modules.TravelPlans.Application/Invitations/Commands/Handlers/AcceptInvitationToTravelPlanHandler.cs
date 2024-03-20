using TravelCompanion.Modules.TravelPlans.Application.Invitations.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Enums;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Invitations;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Modules.TravelPlans.Application.Invitations.Commands.Handlers;

internal sealed class AcceptInvitationToTravelPlanHandler : ICommandHandler<AcceptInvitation>
{
    private readonly IPlanRepository _planRepository;
    private readonly IInvitationRepository _invitationRepository;
    private readonly IMessageBroker _messageBroker;

    public AcceptInvitationToTravelPlanHandler(IPlanRepository planRepository, IInvitationRepository invitationRepository, IMessageBroker messageBroker)
    {
        _planRepository = planRepository;
        _invitationRepository = invitationRepository;
        _messageBroker = messageBroker;
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
        await _messageBroker.PublishAsync(new ParticipantAddedToPlan(invitation.InviteeId, plan.Id));
    }
}