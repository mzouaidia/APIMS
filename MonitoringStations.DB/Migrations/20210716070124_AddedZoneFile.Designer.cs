﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonitoringStations.DB.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MonitoringStations.DB.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210716070124_AddedZoneFile")]
    partial class AddedZoneFile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("MonitoringStations.Domain.Models.Station", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<string>("AddressIp")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Hostname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastModify")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("MacAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("StoreNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Zone")
                        .IsRequired()
                        .HasColumnType("text");

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
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PrinterInfo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PrinterMsg")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PrinterName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PrinterState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("StationId")
                        .HasColumnType("bigint");

                    b.Property<string>("StoreNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StationId");

                    b.ToTable("StationHistory");
                });

            modelBuilder.Entity("MonitoringStations.Domain.Models.StationHistory", b =>
                {
                    b.HasOne("MonitoringStations.Domain.Models.Station", "Station")
                        .WithMany()
                        .HasForeignKey("StationId");

                    b.Navigation("Station");
                });
#pragma warning restore 612, 618
        }
    }
}
