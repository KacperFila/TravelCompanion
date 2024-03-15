using TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Events;
using TravelCompanion.Modules.TravelPlans.Application.Plans.Commands;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Plans;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Modules.TravelPlans.Application.AcceptPlanRequests.Commands.Handlers;

public sealed class AcceptPlanAcceptRequestHandler : ICommandHandler<AcceptPlanAcceptRequest>
{
    private readonly IPlanAcceptRequestRepository _planAcceptRequestRepository;
    private readonly IPlansDomainService _planDomainService;
    private readonly IContext _context;
    private readonly Guid _userId;
    private readonly IMessageBroker _messageBroker;

    public AcceptPlanAcceptRequestHandler(IPlanAcceptRequestRepository planAcceptRequestRepository, IPlansDomainService planDomainService, IContext context, IMessageBroker messageBroker)
    {
        _planAcceptRequestRepository = planAcceptRequestRepository;
        _planDomainService = planDomainService;
        _context = context;
        _messageBroker = messageBroker;
        _userId = _context.Identity.Id;
    }

    public async Task HandleAsync(AcceptPlanAcceptRequest command)
    {
        var request = await _planAcceptRequestRepository.GetByPlanAsync(command.travelPlanId);

        if (request is null)
        {
            throw new AcceptPlanRequestForPlanNotFoundException(command.travelPlanId);
        }

        var planParticipants = await _planDomainService.CheckPlanParticipantsAsync(command.travelPlanId);

        if (!planParticipants.Contains(_userId))
        {
            throw new UserDoesNotParticipateInPlanException(_userId, command.travelPlanId);
        }

        request.AddParticipantAcceptation(_userId);
        await _planAcceptRequestRepository.UpdateAsync(request);
        _messageBroker.PublishAsync(new AcceptPlanRequestParticipantAdded(_userId, command.travelPlanId));
    }
}