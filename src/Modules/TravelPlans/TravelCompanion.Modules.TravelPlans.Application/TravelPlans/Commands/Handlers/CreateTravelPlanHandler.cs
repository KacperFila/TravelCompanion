using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPlans.Commands.Handlers;

public sealed class CreateTravelPlanHandler : ICommandHandler<CreateTravelPlan>
{
    private readonly ITravelPlanRepository _travelPlanRepository;

    public CreateTravelPlanHandler(ITravelPlanRepository travelPlanRepository)
    {
        _travelPlanRepository = travelPlanRepository;
    }

    public async Task HandleAsync(CreateTravelPlan command)
    {
        var travelPlan = TravelPlan.Create(command.Id, command.ownerId, command.title, command.description,
            command.from, command.to);

        await _travelPlanRepository.AddAsync(travelPlan);
    }
}