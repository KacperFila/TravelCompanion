using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
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
        var plan = await _planRepository.GetAsync(@event.PlanId);

        if (plan is null)
        {
            throw new PlanNotFoundException(@event.PlanId);
        }

        var request = await _planAcceptRequestRepository.GetByPlanAsync(plan.Id);

        if (request is null)
        {
            throw new AcceptPlanRequestForPlanNotFoundException(plan.Id);
        }

        var acceptedParticipantsGuids = request.ParticipantsAccepted.Select(x => x.Value).ToList();

        if (plan.Participants.Select(x => x.ParticipantId).All(acceptedParticipantsGuids.Contains) &&
            plan.Participants.Count == request.ParticipantsAccepted.Count)
        {
            plan.ChangeStatusToAccepted();
            await _planRepository.UpdateAsync(plan);
        }
    }
}