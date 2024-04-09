using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using TravelCompanion.Modules.Users.Core.Entities;

namespace TravelCompanion.Modules.Users.Core.Repositories
{
    internal interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<User> GetByTokenAsync(string token);
        Task<List<Guid>> BrowseActiveAsync();
        Task<bool> ExistAsync(Guid id);
        Task<List<string>> GetEmails(List<Guid> usersIds);
        Task<string> GetEmail(Guid userId);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}