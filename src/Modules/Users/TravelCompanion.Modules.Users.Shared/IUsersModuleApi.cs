namespace TravelCompanion.Modules.Users.Shared;

public interface IUsersModuleApi
{
    Task<bool> CheckIfUserExists(Guid userId);
    Task<List<string>> GetUsersEmails(List<Guid> usersIds);
    Task<string> GetUserEmail(Guid userId);
}