using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class InvalidListOfReceiptParticipantsException : TravelCompanionException
{
    public InvalidListOfReceiptParticipantsException() : base($"Given receipt defines invalid participant list.")
    {
    }
}