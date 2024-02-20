﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TravelCompanion.Modules.Travels.Core.DAL;

#nullable disable

namespace TravelCompanion.Modules.Travels.Core.DAL.Migrations
{
    [DbContext(typeof(TravelsDbContext))]
    [Migration("20240220085056_Travels_module_init")]
    partial class Travels_module_init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("travels")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TravelCompanion.Modules.Travels.Core.Entities.Travel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateOnly?>("From")
                        .HasColumnType("date");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<Guid[]>("ParticipantIds")
                        .HasColumnType("uuid[]");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateOnly?>("To")
                        .HasColumnType("date");

                    b.Property<bool>("allParticipantsPaid")
                        .HasColumnType("boolean");

                    b.Property<bool>("isFinished")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Travels", "travels");
                });
#pragma warning restore 612, 618
        }
    }
}
