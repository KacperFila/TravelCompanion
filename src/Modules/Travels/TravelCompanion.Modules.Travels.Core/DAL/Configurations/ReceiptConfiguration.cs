using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.DAL.Configurations;

public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
{
    public void Configure(EntityTypeBuilder<Receipt> builder)
    {
        builder
            .HasKey(x => x.Id);
        
        builder
            .OwnsOne<Money>("Amount", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("Amount")
                .IsRequired();
            money.Property<string>("Currency")
                .HasColumnName("Currency")
                .IsRequired();
        });

        builder
            .Property(x => x.Description)
            .HasMaxLength(120)
            .IsRequired();
    }
}