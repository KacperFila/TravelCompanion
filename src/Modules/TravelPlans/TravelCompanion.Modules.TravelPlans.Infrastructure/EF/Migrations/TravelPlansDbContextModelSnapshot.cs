﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TravelCompanion.Modules.TravelPlans.Infrastructure;

#nullable disable

namespace TravelCompanion.Modules.TravelPlans.Infrastructure.EF.Migrations
{
    [DbContext(typeof(TravelPlansDbContext))]
    partial class TravelPlansDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("InviteeId")
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

                    b.Property<decimal>("AdditionalCostsValue")
                        .HasColumnType("numeric");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("DoesAllParticipantsAccepted")
                        .HasColumnType("boolean");

                    b.Property<bool>("DoesAllParticipantsPaid")
                        .HasColumnType("boolean");

                    b.Property<DateOnly>("From")
                        .HasColumnType("date");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("ParticipantPaidIds")
                        .HasColumnType("uuid[]");

                    b.Property<List<Guid>>("Participants")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly>("To")
                        .HasColumnType("date");

                    b.Property<decimal>("TotalCostValue")
                        .HasColumnType("numeric");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Plans", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.PlanAcceptRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("ParticipantsAccepted")
                        .HasColumnType("uuid[]");

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("PlanAcceptRequests", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Receipt", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<Guid?>("PlanId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PointId")
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("ReceiptParticipants")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.HasIndex("PointId");

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

                    b.Property<Guid>("PlanId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("numeric");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("TravelPoints", "travelPlans");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPointRemoveRequest", b =>
                {
                    b.Property<Guid>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("SuggestedById")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TravelPointId")
                        .HasColumnType("uuid");

                    b.HasKey("RequestId");

                    b.ToTable("TravelPointRemoveRequests", "travelPlans");
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
                    b.HasOne("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Plan", null)
                        .WithMany("AdditionalCosts")
                        .HasForeignKey("PlanId");

                    b.HasOne("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPoint", null)
                        .WithMany("Receipts")
                        .HasForeignKey("PointId")
                        .OnDelete(DeleteBehavior.Cascade);

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
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.Plan", b =>
                {
                    b.Navigation("AdditionalCosts");

                    b.Navigation("TravelPlanPoints");
                });

            modelBuilder.Entity("TravelCompanion.Modules.TravelPlans.Domain.Plans.Entities.TravelPoint", b =>
                {
                    b.Navigation("Receipts");
                });
#pragma warning restore 612, 618
        }
    }
}
