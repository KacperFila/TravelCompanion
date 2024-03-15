using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.DAL.Configurations;

public class TravelPointConfiguration : IEntityTypeConfiguration<TravelPoint>
{
    public void Configure(EntityTypeBuilder<TravelPoint> builder)
    {
        builder
            .HasKey(x => x.TravelPointId);

        builder
            .Property(x => x.TotalCost)
            .HasConversion(x => x.Amount, x => Money.Create(x));

        builder
            .HasMany(x => x.Receipts)
            .WithOne()
            .HasForeignKey(x => x.TravelPointId)
            .OnDelete(DeleteBehavior.ClientCascade);

        builder.Property(x => x.IsVisited)
            .IsRequired();
    }
}