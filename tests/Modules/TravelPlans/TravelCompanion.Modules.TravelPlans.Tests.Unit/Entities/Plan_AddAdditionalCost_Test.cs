using Shouldly;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Tests.Unit.Entities;
public class Plan_AddAdditionalCost_Test
{
    private void Act(Receipt receipt) => _plan.AddAdditionalCost(receipt);

    private readonly AggregateId planId = new AggregateId(Guid.NewGuid());
    private readonly OwnerId ownerId = new OwnerId(Guid.NewGuid());
    private readonly List<Guid> receiptParticipants = Enumerable.Range(0, 5).Select(_ => Guid.NewGuid()).ToList();

    private static Receipt GetReceipt(AggregateId planId, List<Guid> receiptParticipants) =>
        Receipt.Create(receiptParticipants, Money.Create(10), planId, null, "desc");

    [Fact]
    public void receipt_addition_should_success()
    {
        // Arrange - przygotowanie zależności, agregatów itd.
        var receipt = GetReceipt(planId, receiptParticipants);

        // Act - zachowanie 
        var exception = Record.Exception(() => Act(receipt));

        // Assert - weryfikacja wyniku
        exception.ShouldBeNull();
    }
    
    private readonly Plan _plan;

    // Setup - za każdym testem się wykonuje
    public Plan_AddAdditionalCost_Test()
    {
        _plan = new Plan(
            planId,
            ownerId,
            "title",
            "desc",
            new DateOnly(2024, 1, 20),
            new DateOnly(2024,1,25));
    }

}