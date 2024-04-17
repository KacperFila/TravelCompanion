﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TravelCompanion.Modules.Payments.Infrastructure;

#nullable disable

namespace TravelCompanion.Modules.Payments.Infrastructure.EF.Migrations
{
    [DbContext(typeof(PaymentsDbContext))]
    [Migration("20240417045534_Cost_Guid_List")]
    partial class Cost_Guid_List
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("payments")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TravelCompanion.Modules.Payments.Domain.Payments.Entities.ParticipantCost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("ParticipantsIds")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<Guid>("SummaryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SummaryId");

                    b.ToTable("ParticipantsCosts", "payments");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Payments.Domain.Payments.Entities.TravelSummary", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateOnly>("From")
                        .HasColumnType("date");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("PointsAdditionalCost")
                        .HasColumnType("numeric");

                    b.Property<DateOnly>("To")
                        .HasColumnType("date");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("numeric");

                    b.Property<decimal>("TravelAdditionalCost")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TravelId")
                        .HasColumnType("uuid");

                    b.Property<int>("Version")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("TravelSummaries", "payments");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Payments.Domain.Payments.Entities.ParticipantCost", b =>
                {
                    b.HasOne("TravelCompanion.Modules.Payments.Domain.Payments.Entities.TravelSummary", null)
                        .WithMany("ParticipantsCosts")
                        .HasForeignKey("SummaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money.Money", "Value", b1 =>
                        {
                            b1.Property<Guid>("ParticipantCostId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("Value");

                            b1.Property<string>("Currency")
                                .HasColumnType("text");

                            b1.HasKey("ParticipantCostId");

                            b1.ToTable("ParticipantsCosts", "payments");

                            b1.WithOwner()
                                .HasForeignKey("ParticipantCostId");
                        });

                    b.Navigation("Value")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.Payments.Domain.Payments.Entities.TravelSummary", b =>
                {
                    b.Navigation("ParticipantsCosts");
                });
#pragma warning restore 612, 618
        }
    }
}
