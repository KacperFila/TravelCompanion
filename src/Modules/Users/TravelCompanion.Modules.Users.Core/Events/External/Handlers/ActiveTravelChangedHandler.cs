using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Repositories;
using TravelCompanion.Shared.Abstractions.Events;
using TravelCompanion.Shared.Abstractions.RealTime.Travels;

namespace TravelCompanion.Modules.Users.Core.Events.External.Handlers;

internal sealed class ActiveTravelChangedHandler : IEventHandler<ActiveTravelChanged>
{
    private readonly IUserRepository _userRepository;
    private readonly ITravelsRealTimeService _travelsRealTimeService;

    public ActiveTravelChangedHandler(IUserRepository userRepository, ITravelsRealTimeService travelsRealTimeService)
    {
        _userRepository = userRepository;
        _travelsRealTimeService = travelsRealTimeService;
    }
    
    public async Task HandleAsync(ActiveTravelChanged @event)
    {
        var user = await _userRepository.GetAsync(@event.UserId);
        user.SetActiveTravel(@event.TravelId);
        await _userRepository.UpdateAsync(user);

        await _travelsRealTimeService.SendActiveTravelChanged(@event.UserId, @event.TravelId);
    }
}