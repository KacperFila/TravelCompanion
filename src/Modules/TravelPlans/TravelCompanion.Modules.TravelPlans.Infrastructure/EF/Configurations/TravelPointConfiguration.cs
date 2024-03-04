﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal class TravelPointConfiguration : IEntityTypeConfiguration<TravelPoint>
{
    public void Configure(EntityTypeBuilder<TravelPoint> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder.Property(x => x.PlanId)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder
            .HasMany(x => x.Receipts)
            .WithOne()
            .IsRequired();

        builder.Property(x => x.TotalCost)
            .HasConversion(x => x.Amount, x => Money.Create(x));
    }
}