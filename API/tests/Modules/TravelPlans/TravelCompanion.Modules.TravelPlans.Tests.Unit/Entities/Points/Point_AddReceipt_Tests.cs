using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Points;

public class Point_AddReceipt_Tests
{
    private void Act(Receipt receipt) => _point.AddReceipt(receipt);

    private readonly Guid planId = Guid.NewGuid();
    private readonly Guid ownerId = Guid.NewGuid();
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_receipt_is_valid_addition_should_succeed()
    {
        var receipt = GetReceipt(ownerId, planId, receiptParticipants);
        var exception = Record.Exception(() => Act(receipt));

        exception.ShouldBeNull();
        _point.Receipts.ShouldContain(receipt);
    }
    
    private readonly TravelPoint _point;
    public Point_AddReceipt_Tests()
    {
        _point = TravelPoint.Create(
            Guid.NewGuid(),
            "placeName",
            Guid.NewGuid(),
            false);
    }
}