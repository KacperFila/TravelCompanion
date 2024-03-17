using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Events.External.Handlers;

public class AcceptPlanRequestParticipantAddedHandler : IEventHandler<AcceptPlanRequestParticipantAdded>
{
    private readonly IPlanRepository _planRepository;
    private readonly IPlanAcceptRequestRepository _planAcceptRequestRepository;

    public AcceptPlanRequestParticipantAddedHandler(IPlanRepository planRepository, IPlanAcceptRequestRepository planAcceptRequestRepository)
    {
        _planRepository = planRepository;
        _planAcceptRequestRepository = planAcceptRequestRepository;
    }

    public async Task HandleAsync(AcceptPlanRequestParticipantAdded @event)
    {
        var plan = await _planRepository.GetAsync(@event.planId);
        var request = await _planAcceptRequestRepository.GetByPlanAsync(plan.Id);

        if (plan.Participants.SequenceEqual(request.ParticipantsAccepted))
        {
            plan.ChangeStatusToAccepted();
            await _planRepository.UpdateAsync(plan);
        }
    }
}