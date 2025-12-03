using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class TravelPointUpdateRequestConfiguration : IEntityTypeConfiguration<TravelPointUpdateRequest>
{
    public void Configure(EntityTypeBuilder<TravelPointUpdateRequest> builder)
    {
        builder
            .Property(x => x.TravelPlanPointId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder
            .Property(x => x.PlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder
            .Property(x => x.SuggestedById)
            .HasConversion(x => x.Value, x => new EntityId(x));

        builder
            .Property(x => x.RequestId)
            .HasConversion(x => x.Value, x => new TravelPointUpdateRequestId(x));

        builder
            .HasKey(x => x.RequestId);

        builder
            .Property(x => x.PlaceName)
            .IsRequired();
    }
}