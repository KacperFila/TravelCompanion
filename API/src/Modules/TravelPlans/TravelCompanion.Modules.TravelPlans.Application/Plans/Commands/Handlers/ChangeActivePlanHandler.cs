using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.Users.Shared;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

internal class ChangeActivePlanHandler : ICommandHandler<ChangeActivePlan>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly Guid _userId;
    public ChangeActivePlanHandler(
        IContext context,
        IPlanRepository planRepository
        )
    {
        _context = context;
        _userId = _context.Identity.Id;
        _planRepository = planRepository;
    }

    public async Task HandleAsync(ChangeActivePlan command)
    {
        var planExists = await _planRepository.ExistAsync(command.planId);

        if (!planExists)
        {
            throw new PlanNotFoundException(command.planId);
        }

        var plan = await _planRepository.GetAsync(command.planId);

        if (!plan.Participants.Any(x => x.ParticipantId == _userId))
        {
            throw new UserDoesNotParticipateInPlanException(_userId, command.planId);
        }
    }
}