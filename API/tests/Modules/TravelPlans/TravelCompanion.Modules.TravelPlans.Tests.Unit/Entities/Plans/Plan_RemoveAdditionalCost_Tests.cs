using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;
public class PlanRemoveAdditionalCostTests
{
    private void Act(ReceiptId receiptId) => _plan.RemoveAdditionalCost(receiptId);

    private readonly Guid _planId = Guid.NewGuid();
    private readonly Guid _ownerId = Guid.NewGuid();
    private readonly List<Guid> _receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_receipt_with_incorrect_planId_removal_should_fail()
    {
        var receipt = GetReceipt(_ownerId, _planId, _receiptParticipants);
        _plan.AddAdditionalCost(receipt);

        var exception = Record.Exception(() => Act(Guid.NewGuid()));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReceiptNotFoundException>();
    }

    [Fact]
    public void given_receipt_removal_should_succeed()
    {
        var receipt = GetReceipt(_ownerId, _planId, _receiptParticipants);
        _plan.AddAdditionalCost(receipt);

        var exception = Record.Exception(() => Act(receipt.Id));

        exception.ShouldBeNull();
        _plan.AdditionalCosts
            .Where(x => x.Id == receipt.Id)
            .ShouldBeEmpty();
    }

    private readonly Plan _plan;
    public PlanRemoveAdditionalCostTests()
    {
        _plan = new Plan(
            _planId,
            _ownerId,
            "title",
            "desc",
            new DateOnly(2030, 3, 10),
            new DateOnly(2030, 3, 15));
    }

}