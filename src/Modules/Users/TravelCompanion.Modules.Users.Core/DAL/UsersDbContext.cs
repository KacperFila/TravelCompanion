using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Users.Core.Entities;

namespace TravelCompanion.Modules.Users.Core.DAL
{
    internal class UsersDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("users");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
