using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities.Receipts;

public class Receipts_AddReceiptParticipant_Tests
{
    public void Act(Guid participantId) => _receipt.AddReceiptParticipant(participantId);

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
    public Receipts_AddReceiptParticipant_Tests()
    {
        var currentParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

        _receipt = Receipt.Create(
            currentParticipants,
            Money.Create(10),
            Guid.NewGuid(),
            null,
            "desc");
    }
}