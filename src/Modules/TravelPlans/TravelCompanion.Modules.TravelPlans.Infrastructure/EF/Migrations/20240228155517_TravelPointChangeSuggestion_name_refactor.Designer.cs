﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TravelCompanion.Modules.TravelPlans.Infrastructure;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    [DbContext(typeof(TravelPlansDbContext))]
    [Migration("20240228155517_TravelPointChangeSuggestion_name_refactor")]
    partial class TravelPointChangeSuggestion_name_refactor
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("travelPlans")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Invitation", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TravelPlanId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Invitations", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Plan", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("AllParticipantsPaid")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateOnly>("From")
                        .HasColumnType("date");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("ParticipantIds")
                        .HasColumnType("uuid[]");

                    b.Property<List<Guid>>("ParticipantPaidIds")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("To")
                        .HasColumnType("date");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Plans", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Receipt", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ParticipantId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TravelPointCostId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TravelPointCostId");

                    b.ToTable("Receipts", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPoint", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("boolean");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("PlanId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TravelPlanId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("TravelPoints", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPointCost", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TravelPointId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TravelPointCosts", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPointUpdateRequest", b =>
                {
                    b.Property<Guid>("RequestId")
                        .HasColumnType("uuid");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SuggestedById")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TravelPlanPointId")
                        .HasColumnType("uuid");

                    b.HasKey("RequestId");

                    b.ToTable("TravelPointUpdateRequests", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Receipt", b =>
                {
                    b.HasOne("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPointCost", null)
                        .WithMany("Receipts")
                        .HasForeignKey("TravelPointCostId");

                    b.OwnsOne("TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money.Money", "Amount", b1 =>
                        {
                            b1.Property<Guid>("ReceiptId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("Amount");

                            b1.HasKey("ReceiptId");

                            b1.ToTable("Receipts", "travelPlans");

                            b1.WithOwner()
                                .HasForeignKey("ReceiptId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPoint", b =>
                {
                    b.HasOne("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Plan", null)
                        .WithMany("TravelPlanPoints")
                        .HasForeignKey("PlanId");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPointCost", b =>
                {
                    b.OwnsOne("TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money.Money", "OverallCost", b1 =>
                        {
                            b1.Property<Guid>("TravelPointCostId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("OverallCost");

                            b1.HasKey("TravelPointCostId");

                            b1.ToTable("TravelPointCosts", "travelPlans");

                            b1.WithOwner()
                                .HasForeignKey("TravelPointCostId");
                        });

                    b.Navigation("OverallCost")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Plan", b =>
                {
                    b.Navigation("TravelPlanPoints");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPointCost", b =>
                {
                    b.Navigation("Receipts");
                });
#pragma warning restore 612, 618
        }
    }
}