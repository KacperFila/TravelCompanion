using TravelCompanion.Modules.Payments.Domain.Payments.Entities;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Repositories;

public interface ITravelSummaryRepository
{
    public Task AddTravelSummary(TravelSummary travelSummary);
}