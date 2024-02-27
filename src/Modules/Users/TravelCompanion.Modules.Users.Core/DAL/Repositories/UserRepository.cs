using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using TravelCompanion.Modules.Users.Core.Entities;
using TravelCompanion.Modules.Users.Core.Repositories;

namespace TravelCompanion.Modules.Users.Core.DAL.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly UsersDbContext _dbContext;
        private readonly DbSet<User> _users;

        public UserRepository(UsersDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = dbContext.Users;
        }

        public Task<User> GetAsync(Guid id) => _users.SingleOrDefaultAsync(x => x.Id == id);

        public Task<User> GetAsync(string email) => _users.SingleOrDefaultAsync(x => x.Email == email);
        public async Task<bool> ExistAsync(Guid id) => await _users.AnyAsync(x => x.Id == id);

        public async Task AddAsync(User user)
        {
            await _users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}