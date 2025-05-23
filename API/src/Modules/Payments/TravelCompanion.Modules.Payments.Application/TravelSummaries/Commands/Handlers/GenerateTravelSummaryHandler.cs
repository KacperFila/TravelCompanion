using TravelCompanion.Modules.Payments.Domain.Payments.Entities;
using TravelCompanion.Modules.Payments.Domain.Payments.Repositories;
using TravelCompanion.Modules.Travels.Shared;
using TravelCompanion.Modules.Travels.Shared.DTO;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Application.TravelSummaries.Commands.Handlers;

public class GenerateTravelSummaryHandler : ICommandHandler<GenerateTravelSummary>
{
    private readonly ITravelsModuleApi _travelsModuleApi;
    private readonly ITravelSummaryRepository _travelSummaryRepository;

    public GenerateTravelSummaryHandler(ITravelsModuleApi travelsModuleApi, ITravelSummaryRepository travelSummaryRepository)
    {
        _travelsModuleApi = travelsModuleApi;
        _travelSummaryRepository = travelSummaryRepository;
    }

    public async Task HandleAsync(GenerateTravelSummary command)
    {
        var travelDto = await _travelsModuleApi.GetTravelInfo(command.TravelId);
        var summary = GenerateTravelSummary(travelDto);

        await _travelSummaryRepository.AddTravelSummary(summary);
    }

    private TravelSummary GenerateTravelSummary(TravelDto travel)
    {
        var summary = TravelSummary.Create(
            Guid.NewGuid(),
            travel.TravelId,
            travel.From,
            travel.To,
            Money.Create(travel.TotalCostValue),
            Money.Create(travel.TravelAdditionalCostValue),
            Money.Create(travel.PointsAdditionalCostValue));

        return summary;
    }
}

