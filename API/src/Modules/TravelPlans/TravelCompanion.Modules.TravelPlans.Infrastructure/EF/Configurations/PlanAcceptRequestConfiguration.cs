using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class PlanAcceptRequestConfiguration : IEntityTypeConfiguration<PlanAcceptRequest>
{
    public void Configure(EntityTypeBuilder<PlanAcceptRequest> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.PlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x));


        builder
            .Property(x => x.ParticipantsAccepted)
            .HasConversion(
                x => x.Select(a => a.Value).ToList(),
                g => g.Select(g => (EntityId)g).ToList());
    }
}