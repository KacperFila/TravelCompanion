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

        builder.Property(x => x.PointsAdditionalCost)
            .HasConversion(x => x.Amount, x => Money.Create(x));

        builder.Property(x => x.TravelAdditionalCost)
            .HasConversion(x => x.Amount, x => Money.Create(x));

        builder.Property(x => x.TotalCost)
            .HasConversion(x => x.Amount, x => Money.Create(x));

        builder.HasMany(x => x.ParticipantsCosts)
            .WithOne()
            .HasForeignKey(x => x.SummaryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}