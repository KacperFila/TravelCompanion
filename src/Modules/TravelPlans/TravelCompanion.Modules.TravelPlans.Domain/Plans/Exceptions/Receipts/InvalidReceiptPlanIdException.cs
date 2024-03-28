using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class InvalidReceiptPlanIdException : TravelCompanionException
{
    public Guid Id { get; set; }
    public InvalidReceiptPlanIdException(Guid id) : base($"Receipt with Id: {id} defines invalid plan Id.")
    {
        Id = id;
    }
}