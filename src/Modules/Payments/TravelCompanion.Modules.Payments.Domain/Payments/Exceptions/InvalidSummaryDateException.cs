using TravelCompanion.Shared.Abstractions.Exceptions;

namespace TravelCompanion.Modules.Payments.Domain.Payments.Exceptions;

public class InvalidSummaryDateException : TravelCompanionException
{
    public Guid SummaryId { get; }
    public InvalidSummaryDateException(Guid summaryId) : base($"Summary with Id: {summaryId} defines invalid dates.")
    {
        SummaryId = summaryId;
    }
}