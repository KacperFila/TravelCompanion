using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;
public class PlanAddAdditionalCostTests
{
    private void Act(Receipt receipt) => _plan.AddAdditionalCost(receipt);

    private readonly AggregateId _planId = new(Guid.NewGuid());
    private readonly OwnerId _ownerId = (Guid.NewGuid());
    private readonly List<Guid> _receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_receipt_addition_should_succeed()
    {
        var receipt = GetReceipt(_ownerId, _planId, _receiptParticipants);

        var exception = Record.Exception(() => Act(receipt));

        exception.ShouldBeNull();
        _plan.AdditionalCosts.ShouldContain(receipt);
    }

    [Fact]
    public void given_receipt_planId_is_incorrect_addition_should_fail()
    {
        var receipt = GetReceipt(_ownerId, Guid.NewGuid(), _receiptParticipants);

        var exception = Record.Exception(() => Act(receipt));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReceiptPlanIdException>();
    }

    private readonly Plan _plan;
    public PlanAddAdditionalCostTests()
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