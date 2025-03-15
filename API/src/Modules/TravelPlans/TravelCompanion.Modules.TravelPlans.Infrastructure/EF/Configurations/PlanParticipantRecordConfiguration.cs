using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class PlanParticipantRecordConfiguration : IEntityTypeConfiguration<PlanParticipantRecord>
{
    public void Configure(EntityTypeBuilder<PlanParticipantRecord> builder)
    {
        builder.Property(x => x.ParticipantId).IsRequired();
        builder.Property(x => x.TravelPlanId).IsRequired();
        builder.Property(x => x.Id).IsRequired();
    }
}
