using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Travels.Core.Entities;

namespace TravelCompanion.Modules.Travels.Core.DAL;

internal class TravelsDbContext : DbContext
{
    public TravelsDbContext(DbContextOptions<TravelsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Travel> Travels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("travels");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}