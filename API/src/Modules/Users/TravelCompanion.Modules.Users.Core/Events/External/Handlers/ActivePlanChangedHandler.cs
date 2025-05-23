using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Shared.Abstractions.Events;
using TravelCompanion.Shared.Abstractions.RealTime.TravelPlans;

namespace TravelCompanion.Modules.Users.Core.Events.External.Handlers;

internal sealed class ActivePlanChangedHandler : IEventHandler<ActivePlanChanged>
{
    private readonly IUserRepository _userRepository;
    private readonly ITravelPlansRealTimeService _travelPlansRealTimeService;

    public ActivePlanChangedHandler(IUserRepository userRepository, ITravelPlansRealTimeService travelPlansRealTimeService)
    {
        _userRepository = userRepository;
        _travelPlansRealTimeService = travelPlansRealTimeService;
    }

    public async Task HandleAsync(ActivePlanChanged @event)
    {
        var user = await _userRepository.GetAsync(@event.UserId);
        user.SetActivePlan(@event.PlanId);
        await _userRepository.UpdateAsync(user);

        await _travelPlansRealTimeService.SendActivePlanChanged(@event.UserId, @event.PlanId);
    }
}
