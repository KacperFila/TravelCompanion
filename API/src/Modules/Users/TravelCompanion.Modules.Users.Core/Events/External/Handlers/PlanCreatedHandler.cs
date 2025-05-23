using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Shared.Abstractions.Events;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.Users.Core.Events.External.Handlers;

internal sealed class PlanCreatedHandler : IEventHandler<PlanCreated>
{
    private readonly IUserRepository _userRepository;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;

    public PlanCreatedHandler(IUserRepository userRepository, ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _userRepository = userRepository;
        _travelPlansRealTimeService = travelPlansRealTimeService;
    }

    public async Task HandleAsync(PlanCreated @event)
    {
        var user = await _userRepository.GetAsync(@event.ownerId);
        user.SetActivePlan(@event.planId);
        await _userRepository.UpdateAsync(user);

        await _travelPlansRealTimeService.SendActivePlanChanged(@event.ownerId, @event.planId);
    }
}
