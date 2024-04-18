using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Payments.Domain.Payments.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Configurations;

public class PaymentRecordConfiguration : IEntityTypeConfiguration<PaymentRecord>
{
    public void Configure(EntityTypeBuilder<PaymentRecord> builder)
    {
        builder.OwnsOne<Money>("Amount", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("Value")
                .IsRequired();

            money.Property<string>("Currency")
                .HasColumnName("Currency")
                .IsRequired();
        });
    }
}