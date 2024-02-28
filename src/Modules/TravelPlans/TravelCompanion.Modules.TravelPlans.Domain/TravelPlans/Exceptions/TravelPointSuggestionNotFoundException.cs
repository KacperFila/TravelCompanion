using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Exceptions;

public class TravelPointSuggestionNotFoundException : TravelCompanionException
{
    public Guid Id;
    public TravelPointSuggestionNotFoundException(Guid id) : base($"Travel point suggestion with Id: {id} was not found.")
    {
        Id = id;
    }
}