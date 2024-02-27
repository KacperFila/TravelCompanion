using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure;

public class TravelPlansDbContext :DbContext
{
    public TravelPlansDbContext(DbContextOptions<TravelPlansDbContext> options)
    :base(options)
    {
    }

    public DbSet<TravelPlan> TravelPlans { get; set; }
    public DbSet<TravelPoint> TravelPoints { get; set; }
    public DbSet<TravelPlanInvitation> TravelPlanInvitations { get; set; }
    public DbSet<TravelPointCost> TravelPointCosts { get; set; }
    public DbSet<Receipt> Receipts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("travelPlans");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}