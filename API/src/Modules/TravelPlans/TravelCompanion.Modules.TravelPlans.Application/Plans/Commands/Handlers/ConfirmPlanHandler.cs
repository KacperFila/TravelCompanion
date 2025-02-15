using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class ConfirmPlanHandler : ICommandHandler<ConfirmPlan>
{
    private readonly IPlansDomainService _plansDomainService;

    public ConfirmPlanHandler(IPlansDomainService plansDomainService)
    {
        _plansDomainService = plansDomainService;
    }

    public async Task HandleAsync(ConfirmPlan command)
    {
        await _plansDomainService.CreateTravelFromPlan(command.planId);
    }
}