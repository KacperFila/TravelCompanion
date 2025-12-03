using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Entities;

namespace TravelCompanion.Modules.Users.Core.Repositories
{
    internal interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<List<User>> BrowseAsync(List<Guid> usersIds);
        Task<List<User>> BrowseActiveAsync();
        Task<User> GetByTokenAsync(string token);
        Task<List<Guid>> BrowseActiveIdsAsync();
        Task<bool> ExistAsync(Guid id);
        Task<List<string>> GetEmails(List<Guid> usersIds);
        Task<string> GetEmail(Guid userId);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}