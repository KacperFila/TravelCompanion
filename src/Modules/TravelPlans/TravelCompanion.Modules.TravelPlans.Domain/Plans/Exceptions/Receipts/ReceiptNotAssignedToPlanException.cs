using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.TravelPlans.Domain.Plans.Exceptions.Receipts;

public class ReceiptNotAssignedToPlanException : TravelCompanionException
{
    public Guid Id { get; set; }
    public ReceiptNotAssignedToPlanException(Guid id) : base($"Receipt with Id: {id} is not assigned to travel plan.")
    {
        Id = id;
    }
}