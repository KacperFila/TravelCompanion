using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Receipts;

public class Receipt_ChangeDescription_Tests
{
    private void Act(string description) => _receipt.ChangeDescription(description);

    private readonly Guid planId = Guid.NewGuid();
    private readonly Guid ownerId = Guid.NewGuid();
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_description_is_correct_change_should_succeed()
    {
        var description = "new desc";

        var exception = Record.Exception(() => Act(description));

        exception.ShouldBeNull();
        _receipt.Description.ShouldBe(description);
    }

    [Fact]
    public void given_description_is_empty_change_should_fail()
    {
        var description = string.Empty;

        var exception = Record.Exception(() => Act(description));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<EmptyReceiptDescriptionException>();
    }

    private readonly Receipt _receipt;
    public Receipt_ChangeDescription_Tests()
    {
        var currentParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

        _receipt = GetReceipt(ownerId, planId, receiptParticipants);
    }
}