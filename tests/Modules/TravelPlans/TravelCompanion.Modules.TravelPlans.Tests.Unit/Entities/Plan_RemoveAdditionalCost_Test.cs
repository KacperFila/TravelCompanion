using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities;
public class Plan_RemoveAdditionalCost_Test
{
    private void Act(ReceiptId receiptId) => _plan.RemoveAdditionalCost(receiptId);

    private readonly Guid planId = Guid.NewGuid();
    private readonly Guid ownerId = Guid.NewGuid();
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();
    private readonly Plan _plan;

    private static Receipt GetReceipt(AggregateId planId, List<Guid> receiptParticipants) =>
        Receipt.Create(receiptParticipants, Money.Create(10), planId, null, "desc");

    [Fact]
    public void receipt_removal_should_fail()
    {
        // Arrange
        var receipt = GetReceipt(planId, receiptParticipants);
        _plan.AddAdditionalCost(receipt);

        // Act
        var exception = Record.Exception(() => Act(Guid.NewGuid()));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ReceiptNotFoundException>();
    }


    // Setup 
    public Plan_RemoveAdditionalCost_Test()
    {
        _plan = Plan.Create(planId, ownerId, "title", "desc", new DateOnly(2025, 1, 20), new DateOnly(2025, 1, 25));
    }

}