using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Events;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using TravelCompanion.Shared.Abstractions.Messaging;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

internal sealed class AddPointReceiptHandler : ICommandHandler<AddPointReceipt>
{
    private readonly ITravelPointDomainService _travelPointDomainService;
    private readonly IMessageBroker _messageBroker;

    public AddPointReceiptHandler(ITravelPointDomainService travelPointDomainService, IMessageBroker messageBroker)
    {
        _travelPointDomainService = travelPointDomainService;
        _messageBroker = messageBroker;
    }

    public async Task HandleAsync(AddPointReceipt command)
    {
        await _travelPointDomainService.AddReceiptAsync(command.pointId, command.amount, command.receiptParticipants, command.description);
    }
}