using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Payments.Domain.Payments.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Configurations;

public class ParticipantCostConfiguration : IEntityTypeConfiguration<ParticipantCost>
{
    public void Configure(EntityTypeBuilder<ParticipantCost> builder)
    {
        builder.OwnsOne<Money>("Value", money =>
        {
            money.Property<decimal>("Amount")
                .HasColumnName("Value")
                .IsRequired();
        });
    }
}