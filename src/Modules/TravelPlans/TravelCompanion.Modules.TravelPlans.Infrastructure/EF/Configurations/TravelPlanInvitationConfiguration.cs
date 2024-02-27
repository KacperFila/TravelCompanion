using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

public class TravelPlanInvitationConfiguration : IEntityTypeConfiguration<TravelPlanInvitation>
{
    public void Configure(EntityTypeBuilder<TravelPlanInvitation> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new TravelPlanInvitationId(x));

        builder.Property(x => x.ParticipantId)
            .HasConversion(x => x.Value, x => new ParticipantId(x));

        builder.Property(x => x.TravelPlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x));
    }
}