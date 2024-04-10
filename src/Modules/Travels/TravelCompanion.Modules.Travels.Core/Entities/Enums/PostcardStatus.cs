namespace TravelCompanion.Modules.Travels.Core.Entities.Enums;

public static class PostcardStatus
{
    public const string Accepted = nameof(Accepted);
    public const string Rejected = nameof(Rejected);
    public const string Pending = nameof(Pending);

    public static bool IsValid(string status)
        => status is Accepted or Rejected or Pending;
}