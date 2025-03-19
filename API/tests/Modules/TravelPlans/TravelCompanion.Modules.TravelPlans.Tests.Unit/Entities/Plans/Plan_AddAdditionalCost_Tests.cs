using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Plans;
public class Plan_AddAdditionalCost_Tests
{
    private void Act(Receipt receipt) => _plan.AddAdditionalCost(receipt);

    private readonly AggregateId planId = new(Guid.NewGuid());
    private readonly OwnerId ownerId = (Guid.NewGuid());
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_receipt_addition_should_succeed()
    {
        var receipt = GetReceipt(ownerId, planId, receiptParticipants);

        var exception = Record.Exception(() => Act(receipt));

        exception.ShouldBeNull();
        _plan.AdditionalCosts.ShouldContain(receipt);
    }

    [Fact]
    public void given_receipt_planId_is_incorrect_addition_should_fail()
    {
        var receipt = GetReceipt(ownerId, Guid.NewGuid(), receiptParticipants);

        var exception = Record.Exception(() => Act(receipt));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReceiptPlanIdException>();
    }

    private readonly Plan _plan;
    public Plan_AddAdditionalCost_Tests()
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