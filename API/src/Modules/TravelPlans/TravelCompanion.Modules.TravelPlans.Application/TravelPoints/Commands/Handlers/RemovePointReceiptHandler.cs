using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Points;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Repositories;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Contexts;

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