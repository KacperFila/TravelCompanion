using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure;

public class TravelPlansDbContext :DbContext
{
    public TravelPlansDbContext(DbContextOptions<TravelPlansDbContext> options)
    :base(options)
    {
    }

    public DbSet<Plan> Plans { get; set; }
    public DbSet<TravelPoint> TravelPoints { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<TravelPointUpdateRequest> TravelPointUpdateRequests { get; set; }
    public DbSet<TravelPointRemoveRequest> TravelPointRemoveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("travelPlans");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}