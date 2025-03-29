using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelCompanion.Modules.Users.Core.Entities;
using TravelCompanion.Modules.Users.Core.Repositories;

namespace TravelCompanion.Modules.Users.Core.DAL.Repositories;

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
    public async Task<User> GetByTokenAsync(string token) => await _users.SingleOrDefaultAsync(x => x.VerificationToken == token);
    public async Task<List<Guid>> BrowseActiveAsync()
        => await _users
            .Where(x => x.IsActive)
            .Select(x => x.Id)
            .ToListAsync();

    public async Task<bool> ExistAsync(Guid id) => await _users.AnyAsync(x => x.Id == id);

    public async Task<List<string>> GetEmails(List<Guid> usersIds) =>
        await _users.Where(x => usersIds.Contains(x.Id)).Select(x => x.Email).ToListAsync();

    public async Task<string> GetEmail(Guid userId)
    {
        var user = await _users.SingleOrDefaultAsync(x => x.Id == userId);
        return user.Email;
    }

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

    public async Task<List<User>> BrowseAsync(List<Guid> usersIds)
    {
        return await _users.Where(x => usersIds.Contains(x.Id)).ToListAsync();
    }
}