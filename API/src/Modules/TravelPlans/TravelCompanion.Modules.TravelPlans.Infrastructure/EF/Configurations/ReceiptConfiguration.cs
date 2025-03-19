using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new ReceiptId(x));

        builder.Property(x => x.ReceiptOwnerId)
            .HasConversion(x => x.Value, x => new OwnerId(x));

        builder.Property(x => x.PlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder.Property(x => x.PointId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder.Property(x => x.Description)
            .HasMaxLength(120)
            .IsRequired();

        builder.OwnsOne<Money>("Amount", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("Amount")
                .IsRequired();
            money.Property<string>("Currency")
                .HasColumnName("Currency")
                .IsRequired();
        });
    }
}