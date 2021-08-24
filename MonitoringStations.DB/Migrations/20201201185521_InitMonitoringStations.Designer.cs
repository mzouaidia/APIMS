﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonitoringStations.DB.Context;

namespace MonitoringStations.DB.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201201185521_InitMonitoringStations")]
    partial class InitMonitoringStations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MonitoringStations.Domain.Models.Station", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("AddressIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Hostname")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("datetime2");

                    b.Property<string>("MacAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StoreNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Hostname", "MacAddress")
                        .IsUnique();

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("MonitoringStations.Domain.Models.StationHistory", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("AddressIp")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PrinterNotify")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrinterStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("StationId")
                        .HasColumnType("bigint");

                    b.Property<string>("StoreNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StationId");

                    b.ToTable("StationHistory");
                });

            modelBuilder.Entity("MonitoringStations.Domain.Models.StationHistory", b =>
                {
                    b.HasOne("MonitoringStations.Domain.Models.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId");
                });
#pragma warning restore 612, 618
        }
    }
}