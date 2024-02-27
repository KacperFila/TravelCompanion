namespace TravelCompanion.Modules.Users.Shared;

public interface IUsersModuleApi
{
    Task<bool> CheckIfUserExists(Guid  userId);
}