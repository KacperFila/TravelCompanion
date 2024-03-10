using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Travels.Core.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Travels.Core.DAL.Configurations;

internal class TravelConfiguration : IEntityTypeConfiguration<Travel>
{
    public void Configure(EntityTypeBuilder<Travel> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany(x => x.AdditionalCosts)
            .WithOne()
            .HasForeignKey(x => x.TravelId);

        builder
            .HasMany(x => x.TravelPoints)
            .WithOne()
            .HasForeignKey(x => x.TravelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(x => x.AdditionalCostsValue)
            .HasConversion(x => x.Amount, x => Money.Create(x));
    }
}