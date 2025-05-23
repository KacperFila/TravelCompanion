using TravelCompanion.Modules.TravelPlans.Application.Plans.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Modules.TravelPlans.Application.Plans.Commands.Handlers;

public sealed class CreateTravelPlanHandler : ICommandHandler<CreateTravelPlan>
{
    private readonly IPlanRepository _planRepository;
    private readonly IContext _context;
    private readonly IMessageBroker _messageBroker;
    private readonly Guid _userId;

    public CreateTravelPlanHandler(IPlanRepository planRepository, IContext context, IMessageBroker messageBroker)
    {
        _planRepository = planRepository;
        _context = context;
        _userId = _context.Identity.Id;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(CreateTravelPlan command)
    {
        var travelPlan = Plan.Create(
            _userId,
            command.Title,
            command.Description,
            command.From,
            command.To);

        if(command.Title is null || command.Title.Length < 3)
        {
            throw new InvalidPlanTitleException();
        }

        await _planRepository.AddAsync(travelPlan);
        await _messageBroker.PublishAsync(new PlanCreated(_userId, travelPlan.Id));
    }
}