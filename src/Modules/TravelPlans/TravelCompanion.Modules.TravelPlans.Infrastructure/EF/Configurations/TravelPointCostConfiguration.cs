using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal class TravelPointCostConfiguration : IEntityTypeConfiguration<TravelPointCost>
{
    public void Configure(EntityTypeBuilder<TravelPointCost> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new EntityId(x));

        builder.Property(x => x.TravelPointId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder
            .HasOne<TravelPoint>()
            .WithOne(x => x.TravelPointCost)
            .HasForeignKey<TravelPointCost>(x => x.TravelPointId);

        builder.Property(x => x.OverallCost)
            .HasConversion(x => x.Amount, x => Money.Create(x));
    }
}