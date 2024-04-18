using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class InvalidReceiptOwnerException : TravelCompanionException
{
    public Guid Id { get; set; }
    public InvalidReceiptOwnerException(Guid id) : base($"Receipt with Id: {id} defines invalid owner.")
    {
        Id = id;
    }
}