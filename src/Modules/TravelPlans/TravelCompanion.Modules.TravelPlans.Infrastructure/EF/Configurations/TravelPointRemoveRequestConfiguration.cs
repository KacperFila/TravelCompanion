using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class TravelPointRemoveRequestConfiguration : IEntityTypeConfiguration<TravelPointRemoveRequest>
{
    public void Configure(EntityTypeBuilder<TravelPointRemoveRequest> builder)
    {
        builder
            .HasKey(x => x.RequestId);

        builder
            .Property(x => x.TravelPointId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder
            .Property(x => x.SuggestedById)
            .HasConversion(x => x.Value, x => new EntityId(x));

        builder
            .HasKey(x => x.RequestId);
    }
}