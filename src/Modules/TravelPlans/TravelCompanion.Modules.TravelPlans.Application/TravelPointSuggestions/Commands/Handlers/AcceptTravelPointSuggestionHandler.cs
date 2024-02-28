using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPointSuggestions.Commands.Handlers;

internal class AcceptTravelPointSuggestionHandler : ICommandHandler<AcceptTravelPointSuggestion>
{
    private readonly ITravelPointRepository _travelPointRepository;
    private readonly ITravelPointSuggestionsRepository _travelPointSuggestionsRepository;
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;

    public AcceptTravelPointSuggestionHandler(ITravelPointRepository travelPointRepository, ITravelPointSuggestionsRepository travelPointSuggestionsRepository, IContext context, IPlanRepository planRepository)
    {
        _travelPointRepository = travelPointRepository;
        _travelPointSuggestionsRepository = travelPointSuggestionsRepository;
        _context = context;
        _planRepository = planRepository;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(AcceptTravelPointSuggestion command)
    {
        var suggestion = await _travelPointSuggestionsRepository.GetAsync(command.suggestionId);
        
        if (suggestion is null)
        {
            throw new TravelPointSuggestionNotFoundException(command.suggestionId);
        }

        var travelPoint = await _travelPointRepository.GetAsync(suggestion.TravelPlanPointId);

        if (travelPoint is null)
        {
            throw new TravelPointNotFoundException(travelPoint.Id);
        }

        var travelPlan = await _planRepository.GetAsync(travelPoint.TravelPlanId);

        if (travelPlan.OwnerId != _userId)
        {
            throw new UserNotAllowedToChangeTravelPointException();
        }

        travelPoint.ChangeTravelPointPlaceName(suggestion.PlaceName);

        await _travelPointRepository.UpdateAsync(travelPoint);
        await _travelPointSuggestionsRepository.RemoveAsync(suggestion);
    }
}