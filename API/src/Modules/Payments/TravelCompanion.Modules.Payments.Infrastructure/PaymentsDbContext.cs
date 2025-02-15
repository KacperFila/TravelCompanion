using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.Payments.Domain.Payments.Entities;

namespace TravelCompanion.Modules.Payments.Infrastructure;

public class PaymentsDbContext : DbContext
{
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options)
    : base(options)
    {
    }

    public DbSet<TravelSummary> TravelSummaries { get; set; }
    public DbSet<ParticipantCost> ParticipantsCosts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("payments");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}