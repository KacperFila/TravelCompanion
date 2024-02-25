﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal class TravelPlanConfiguration : IEntityTypeConfiguration<TravelPlan>
{
    public void Configure(EntityTypeBuilder<TravelPlan> builder)
    {
        builder.Property(x => x.OwnerId)
            .HasConversion(x => x.Value, x => new OwnerId(x));

        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder.Property(x => x.ParticipantIds)
            .HasConversion(x => x.Select(a => a.Value).ToList(), g => g.Select(g => (ParticipantId)g).ToList());

        builder.Property(x => x.ParticipantPaidIds)
            .HasConversion(x => x.Select(a => a.Value).ToList(), g => g.Select(g => (ParticipantId)g).ToList());
    }
}