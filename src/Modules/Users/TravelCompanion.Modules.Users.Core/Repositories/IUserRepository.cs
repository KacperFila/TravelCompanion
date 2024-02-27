using System.Threading.Tasks;
using System;
using TravelCompanion.Modules.Users.Core.Entities;

namespace TravelCompanion.Modules.Users.Core.Repositories
{
    internal interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<bool> ExistAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}