using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

public class RemovePointReceiptHandler : ICommandHandler<RemovePointReceipt>
{
    private readonly ITravelPointDomainService _travelPointDomainService;

    public RemovePointReceiptHandler(ITravelPointDomainService travelPointDomainService)
    {
        _travelPointDomainService = travelPointDomainService;
    }
    public async Task HandleAsync(RemovePointReceipt command)
    {
        await _travelPointDomainService.RemoveReceiptAsync(command.receiptId);
    }
}