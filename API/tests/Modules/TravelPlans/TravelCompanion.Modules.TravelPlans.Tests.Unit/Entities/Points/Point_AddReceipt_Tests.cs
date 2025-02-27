using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Points;

public class Point_AddReceipt_Tests
{
    private void Act(Receipt receipt) => _point.AddReceipt(receipt);

    [Fact]
    public void given_receipt_is_valid_addition_should_succeed()
    {
        var receipt = Receipt.Create(Guid.NewGuid(), new List<Guid>(), Money.Create(10), null, _point.Id, "desc");

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