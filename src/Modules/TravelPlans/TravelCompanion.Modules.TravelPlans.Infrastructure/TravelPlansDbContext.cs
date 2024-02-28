﻿using Microsoft.EntityFrameworkCore;
using TravelCompanion.Modules.TravelPlans.Domain.TravelPlans.Entities;

namespace TravelCompanion.Modules.TravelPlans.Infrastructure;

public class TravelPlansDbContext :DbContext
{
    public TravelPlansDbContext(DbContextOptions<TravelPlansDbContext> options)
    :base(options)
    {
    }

    public DbSet<TravelPlan> TravelPlans { get; set; }
    public DbSet<TravelPoint> TravelPoints { get; set; }
    public DbSet<Invitation> Invitations { get; set; }
    public DbSet<TravelPointCost> TravelPointCosts { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<TravelPointChangeSuggestion> TravelPointChangeSuggestions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("travelPlans");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}