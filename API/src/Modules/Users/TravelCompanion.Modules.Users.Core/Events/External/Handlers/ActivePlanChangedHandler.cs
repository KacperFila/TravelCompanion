
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Shared.Abstractions.Events;

namespace TravelCompanion.Modules.Users.Core.Events.External.Handlers;

internal sealed class ActivePlanChangedHandler : IEventHandler<ActivePlanChanged>
{
    private readonly IUserRepository _userRepository;

    public ActivePlanChangedHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task HandleAsync(ActivePlanChanged @event)
    {
        var user = await _userRepository.GetAsync(@event.userId);
        user.SetActivePlan(@event.planId);
        await _userRepository.UpdateAsync(user);
    }
}
