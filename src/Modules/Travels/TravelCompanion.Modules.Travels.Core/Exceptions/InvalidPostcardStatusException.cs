using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Travels.Core.Exceptions;

public class InvalidPostcardStatusException : TravelCompanionException
{
    public string Status { get; set; }
    public InvalidPostcardStatusException(string status) : base($"Given postcard status is invalid: {status}")
    {
        Status = status;
    }
}