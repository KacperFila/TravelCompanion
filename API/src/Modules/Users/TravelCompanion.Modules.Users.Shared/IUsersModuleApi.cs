﻿using TravelCompanion.Modules.Users.Shared.DTO;

namespace TravelCompanion.Modules.Users.Shared;

public interface IUsersModuleApi
{
    Task<bool> CheckIfUserExists(Guid userId);
    Task<List<string>> GetUsersEmails(List<Guid> usersIds);
    Task<string> GetUserEmail(Guid userId);
    Task<List<Guid>> GetUsersIdsAsync();
    Task<UserInfoDto> GetUserInfo(Guid userId);
    Task<List<UserInfoDto>> BrowseUsersInfoAsync(List<Guid> usersIds);
}