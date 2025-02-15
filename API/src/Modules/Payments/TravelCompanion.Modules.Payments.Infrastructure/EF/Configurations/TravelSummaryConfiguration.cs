using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Payments.Domain.Payments.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Configurations;

public class TravelSummaryConfiguration : IEntityTypeConfiguration<TravelSummary>
{
    public void Configure(EntityTypeBuilder<TravelSummary> builder)
    {
        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));
        
        builder.HasMany(x => x.ParticipantsCosts)
            .WithOne()
            .HasForeignKey(x => x.SummaryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.OwnsOne<Money>("TotalCost", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("TotalCostValue")
                .IsRequired();

            money.Property<string>("Currency")
                .HasColumnName("TotalCostCurrency")
                .IsRequired();
        });

        builder.OwnsOne<Money>("TravelAdditionalCost", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("TravelAdditionalCostValue")
                .IsRequired();

            money.Property<string>("Currency")
                .HasColumnName("TravelAdditionalCostCurrency")
                .IsRequired();
        });

        builder.OwnsOne<Money>("PointsAdditionalCost", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("PointsAdditionalCostValue")
                .IsRequired();

            money.Property<string>("Currency")
                .HasColumnName("PointsAdditionalCostCurrency")
                .IsRequired();
        });
    }
}