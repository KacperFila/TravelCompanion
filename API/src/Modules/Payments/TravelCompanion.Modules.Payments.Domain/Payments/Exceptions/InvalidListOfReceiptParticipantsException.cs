using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;

public class InvalidListOfReceiptParticipantsException : TravelCompanionException
{
    public InvalidListOfReceiptParticipantsException() : base($"Given receipt defines invalid participant list.")
    {
    }
}