using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Receipts;

public class ReceiptsAddReceiptParticipantTests
{
    public void Act(Guid participantId) => _receipt.AddReceiptParticipant(participantId);

    private readonly Guid _planId = Guid.NewGuid();
    private readonly Guid _ownerId = Guid.NewGuid();
    private readonly List<Guid> _receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(OwnerId ownerId, AggregateId planId, List<Guid> receiptParticipants)
    {
        receiptParticipants.Add(ownerId);
        return Receipt.Create(ownerId, receiptParticipants, Money.Create(10), planId, null, "desc");
    }

    [Fact]
    public void given_participant_is_correct_addition_should_succeed()
    {
        var participantId = Guid.NewGuid();

        var exception = Record.Exception(() => Act(participantId));

        exception.ShouldBeNull();
        _receipt.ReceiptParticipants.ShouldContain(participantId);
    }

    [Fact]
    public void given_participant_is_already_added_addition_should_fail()
    {
        var participantId = Guid.NewGuid();
        _receipt.AddReceiptParticipant(participantId);

        var exception = Record.Exception(() => Act(participantId));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ParticipantAlreadyAddedException>();
    }

    private readonly Receipt _receipt;
    public ReceiptsAddReceiptParticipantTests()
    {
        var currentParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

        _receipt = GetReceipt(_ownerId, _planId, _receiptParticipants);
    }
}