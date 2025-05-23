using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

internal sealed class AddPointReceiptHandler : ICommandHandler<AddPointReceipt>
{
    private readonly ITravelPointDomainService _travelPointDomainService;

    public AddPointReceiptHandler(ITravelPointDomainService travelPointDomainService)
    {
        _travelPointDomainService = travelPointDomainService;
    }

    public async Task HandleAsync(AddPointReceipt command)
    {
        await _travelPointDomainService.AddReceiptAsync(command.PointId, command.Amount, command.ReceiptParticipants, command.Description);
    }
}