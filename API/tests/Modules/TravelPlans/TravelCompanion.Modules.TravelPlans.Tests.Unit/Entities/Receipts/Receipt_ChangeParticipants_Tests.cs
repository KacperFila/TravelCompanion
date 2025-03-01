using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Receipts;

public class Receipt_ChangeParticipants_Tests
{
    private void Act(List<Guid> participants) => _receipt.ChangeReceiptParticipants(participants);

    private readonly Guid planId = Guid.NewGuid();
    private readonly Guid ownerId = Guid.NewGuid();
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_participants_are_valid_change_should_succeed()
    {
        var newParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

        var exception = Record.Exception(() => Act(newParticipants));

        exception.ShouldBeNull();
        _receipt.ReceiptParticipants.ShouldBe(newParticipants);
    }

    [Fact]
    public void given_participants_are_empty_change_should_fail()
    {
        var newParticipants = new List<Guid>();

        var exception = Record.Exception(() => Act(newParticipants));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReceiptParametersException>();
    }

    private readonly Receipt _receipt;
    public Receipt_ChangeParticipants_Tests()
    {
        var currentParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

        _receipt = GetReceipt(ownerId, planId, receiptParticipants);
    }
}