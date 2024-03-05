using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Services;
using TravelCompanion.Shared.Abstractions.Commands;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Application.TravelPoints.Commands.Handlers;

internal sealed class AddPointReceiptHandler : ICommandHandler<AddPointReceipt>
{
    private readonly IPlansDomainService _plansDomainService;

    public AddPointReceiptHandler(IPlansDomainService plansDomainService)
    {
        _plansDomainService = plansDomainService;
    }

    public async Task HandleAsync(AddPointReceipt command)
    {
        var receipt = Receipt.Create(
            command.participantId,
            Money.Create(command.amount),
            null,
            command.pointId);

        await _plansDomainService.AddReceiptAsync(receipt);
    }
}