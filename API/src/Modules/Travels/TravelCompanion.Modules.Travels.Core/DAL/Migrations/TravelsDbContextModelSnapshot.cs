﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TravelCompanion.Modules.Travels.Core.DAL;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    [DbContext(typeof(TravelsDbContext))]
    partial class TravelsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("travels")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.Postcard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddedById")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhotoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("TravelId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TravelId");

                    b.ToTable("Postcards", "travels");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.Receipt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("character varying(120)");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ReceiptOwnerId")
                        .HasColumnType("uuid");

                    b.Property<List<Guid>>("ReceiptParticipants")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<Guid?>("TravelId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TravelPointId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TravelId");

                    b.HasIndex("TravelPointId");

                    b.ToTable("Receipts", "travels");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.Travel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("AdditionalCostsValue")
                        .HasColumnType("numeric");

                    b.Property<bool>("AllParticipantsPaid")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("From")
                        .HasColumnType("date");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid[]>("ParticipantIds")
                        .HasColumnType("uuid[]");

                    b.Property<float?>("RatingValue")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("To")
                        .HasColumnType("date");

                    b.Property<decimal>("TotalCostsValue")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Travels", "travels");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.TravelPoint", b =>
                {
                    b.Property<Guid>("TravelPointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsVisited")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PlaceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TravelId")
                        .HasColumnType("uuid");

                    b.HasKey("TravelPointId");

                    b.HasIndex("TravelId");

                    b.ToTable("TravelPoints", "travels");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.TravelRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TravelId")
                        .HasColumnType("uuid");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("TravelId");

                    b.ToTable("TravelRating", "travels");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.Postcard", b =>
                {
                    b.HasOne("TravelCompanion.Modules.Travels.Core.Entities.Travel", null)
                        .WithMany()
                        .HasForeignKey("TravelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.Receipt", b =>
                {
                    b.HasOne("TravelCompanion.Modules.Travels.Core.Entities.Travel", null)
                        .WithMany("AdditionalCosts")
                        .HasForeignKey("TravelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelCompanion.Modules.Travels.Core.Entities.TravelPoint", null)
                        .WithMany("Receipts")
                        .HasForeignKey("TravelPointId")
                        .OnDelete(DeleteBehavior.ClientCascade);

                    b.OwnsOne("TravelCompanion.Modules.Travels.Core.Entities.Receipt.Amount#TravelCompanion.Shared.Abstractions.Kernel.ValueObjects.Money.Money", "Amount", b1 =>
                        {
                            b1.Property<Guid>("ReceiptId")
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("Amount");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Currency");

                            b1.HasKey("ReceiptId");

                            b1.ToTable("Receipts", "travels");

                            b1.WithOwner()
                                .HasForeignKey("ReceiptId");
                        });

                    b.Navigation("Amount")
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.TravelPoint", b =>
                {
                    b.HasOne("TravelCompanion.Modules.Travels.Core.Entities.Travel", null)
                        .WithMany("TravelPoints")
                        .HasForeignKey("TravelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.TravelRating", b =>
                {
                    b.HasOne("TravelCompanion.Modules.Travels.Core.Entities.Travel", null)
                        .WithMany("Ratings")
                        .HasForeignKey("TravelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.Travel", b =>
                {
                    b.Navigation("AdditionalCosts");

                    b.Navigation("Ratings");

                    b.Navigation("TravelPoints");
                });

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.TravelPoint", b =>
                {
                    b.Navigation("Receipts");
                });
#pragma warning restore 612, 618
        }
    }
}
