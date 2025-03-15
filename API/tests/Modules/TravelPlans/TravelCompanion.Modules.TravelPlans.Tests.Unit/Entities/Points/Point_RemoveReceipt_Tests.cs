using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Points;

public class Point_RemoveReceipt_Tests
{
    private void Act(Receipt receipt) => _point.RemoveReceipt(receipt);

    private readonly Guid planId = Guid.NewGuid();
    private readonly Guid ownerId = Guid.NewGuid();
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_receipt_is_added_removal_should_succeed()
    {
        _point.AddReceipt(_receipt);

        var exception = Record.Exception(() => Act(_receipt));
        
        exception.ShouldBeNull();
        _point.Receipts.ShouldNotContain(_receipt);
    }

    [Fact]
    public void given_receipt_is_not_added_removal_should_fail()
    {
        var exception = Record.Exception(() => Act(_receipt));
        
        exception.ShouldBeOfType<ReceiptNotFoundException>();
    }

    private readonly TravelPoint _point;
    private readonly Receipt _receipt;
    public Point_RemoveReceipt_Tests()
    {
        _point = TravelPoint.Create(
            Guid.NewGuid(),
            "placeName",
            Guid.NewGuid(),
            false,
            0);

        _receipt = GetReceipt(ownerId, planId, receiptParticipants);
    }
}