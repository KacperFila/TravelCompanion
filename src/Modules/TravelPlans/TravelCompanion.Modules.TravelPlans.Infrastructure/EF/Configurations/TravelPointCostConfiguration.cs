using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal class TravelPointCostConfiguration : IEntityTypeConfiguration<TravelPointCost>
{
    public void Configure(EntityTypeBuilder<TravelPointCost> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder.Property(x => x.TravelPointId)
            .HasConversion(x => x.Value, x => new TravelPointId(x));

        builder.OwnsOne<Money>("OverallCost", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("OverallCost")
                .IsRequired();
        });
    }
}