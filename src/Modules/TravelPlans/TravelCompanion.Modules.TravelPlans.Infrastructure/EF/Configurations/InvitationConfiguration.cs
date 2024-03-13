using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    public void Configure(EntityTypeBuilder<Invitation> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new InvitationId(x));

        builder.Property(x => x.InviteeId)
            .HasConversion(x => x.Value, x => new EntityId(x));

        builder.Property(x => x.TravelPlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x));
    }
}