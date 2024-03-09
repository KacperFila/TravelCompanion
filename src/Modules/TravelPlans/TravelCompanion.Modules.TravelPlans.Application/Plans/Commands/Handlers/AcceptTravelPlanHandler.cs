using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public class AcceptTravelPlanHandler : ICommandHandler<AcceptTravelPlan>
{
    private readonly IPlansDomainService _plansDomainService;

    public AcceptTravelPlanHandler(IPlansDomainService plansDomainService)
    {
        _plansDomainService = plansDomainService;
    }

    public async Task HandleAsync(AcceptTravelPlan command)
    {
        await _plansDomainService.AcceptTravelPlan(command.travelPlanId);
    }
}