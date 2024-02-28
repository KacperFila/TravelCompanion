﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal class TravelPointConfiguration : IEntityTypeConfiguration<TravelPoint>
{
    public void Configure(EntityTypeBuilder<TravelPoint> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder.Property(x => x.TravelPlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x));
    }
}