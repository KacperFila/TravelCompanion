﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Text.Json;
using TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities;
using TravelCompanion.Shared.Abstractions.Kernel.Types;
using TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Configurations;

internal sealed class PlanConfiguration : IEntityTypeConfiguration<Plan>
{
    public void Configure(EntityTypeBuilder<Plan> builder)
    {
        builder
            .Property(x => x.OwnerId)
            .HasConversion(x => x.Value, x => new OwnerId(x));

        builder
            .Property(x => x.Id)
            .HasConversion(x => x.Value, x => new AggregateId(x));

        builder
                .Property(x => x.ParticipantPaidIds)
                .HasConversion(
                    x => x.Select(a => a.Value).ToList(),
                    g => g.Select(g => (EntityId)g).ToList());

        builder
            .HasMany(x => x.AdditionalCosts)
            .WithOne()
            .HasForeignKey(x => x.PlanId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Property(x => x.AdditionalCostsValue)
            .HasConversion(x => x.Amount, x => Money.Create(x));

        builder
            .Property(x => x.TotalCostValue)
            .HasConversion(x => x.Amount, x => Money.Create(x));

    }
}