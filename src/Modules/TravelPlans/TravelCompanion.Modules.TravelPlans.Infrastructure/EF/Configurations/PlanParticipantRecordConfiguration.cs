using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class PlanParticipantRecordConfiguration : IEntityTypeConfiguration<PlanParticipantRecord>
{
    public void Configure(EntityTypeBuilder<PlanParticipantRecord> builder)
    {
        builder.Property(x => x.ParticipantId)
            .IsRequired();

        builder.Property(x => x.PlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x))
            .ValueGeneratedNever();

        builder.Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedNever();
    }
}
