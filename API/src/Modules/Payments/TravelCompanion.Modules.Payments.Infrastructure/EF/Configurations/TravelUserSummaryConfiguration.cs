using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.Payments.Domain.Payments.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Configurations;

public class TravelUserSummaryConfiguration : IEntityTypeConfiguration<TravelUserSummary>
{
    public void Configure(EntityTypeBuilder<TravelUserSummary> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));
    }
}