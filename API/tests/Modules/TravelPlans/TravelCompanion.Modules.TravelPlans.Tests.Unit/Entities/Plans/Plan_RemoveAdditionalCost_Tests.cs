using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;
using TravelCompanion.Shared.Abstractions.Time;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;
public class Plan_RemoveAdditionalCost_Tests
{
    private void Act(ReceiptId receiptId) => _plan.RemoveAdditionalCost(receiptId);

    private readonly Guid planId = Guid.NewGuid();
    private readonly Guid ownerId = Guid.NewGuid();
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(AggregateId planId, List<Guid> receiptParticipants) =>
        Receipt.Create(receiptParticipants, Money.Create(10), planId, null, "desc");

    [Fact]
    public void given_receipt_with_incorrect_planId_removal_should_fail()
    {
        var receipt = GetReceipt(planId, receiptParticipants);
        _plan.AddAdditionalCost(receipt);

        var exception = Record.Exception(() => Act(Guid.NewGuid()));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReceiptNotFoundException>();
    }

    [Fact]
    public void given_receipt_removal_should_succeed()
    {
        var receipt = GetReceipt(planId, receiptParticipants);
        _plan.AddAdditionalCost(receipt);

        var exception = Record.Exception(() => Act(receipt.Id));

        exception.ShouldBeNull();
        _plan.AdditionalCosts
            .Where(x => x.Id == receipt.Id)
            .ShouldBeEmpty();
    }

    private readonly Plan _plan;
    public Plan_RemoveAdditionalCost_Tests()
    {
        _plan = new Plan(
            planId,
            ownerId,
            "title",
            "desc",
            new DateOnly(2030, 3, 10),
            new DateOnly(2030, 3, 15));
    }

}