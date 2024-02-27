using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

public sealed class TravelPointChangeSuggestion
{
    public TravelPointChangeSuggestionId SuggestionId { get; private set; }
    public AggregateId TravelPlanPointId { get; private set; }
    public ParticipantId SuggestedById { get; private set; }
    public string PlaceName { get; private set; }

    public TravelPointChangeSuggestion(AggregateId travelPlanPointId, ParticipantId suggestedById)
    {
        SuggestionId = Guid.NewGuid();
        TravelPlanPointId = travelPlanPointId;
        SuggestedById = suggestedById;
    }

    public static TravelPointChangeSuggestion Create(AggregateId travelPlanPointId, ParticipantId suggestedById, string PlaceName)
    {
        var suggestion = new TravelPointChangeSuggestion(travelPlanPointId, suggestedById);
        suggestion.PlaceName = PlaceName;

        return suggestion;
    }

    public void ChangePlaceName(string placeName)
    {
        if (string.IsNullOrEmpty(placeName))
        {
            throw new EmptyTravelPointPlaceNameException(TravelPlanPointId);
        }

        PlaceName = placeName;
    }
}