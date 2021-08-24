using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MonitoringStations.Common.Logs;
using MonitoringStations.Domain.Models;

namespace MonitoringStations.DB.Context
{
    public class DataContext : IdentityDbContext //DbContext
    {
        public DataContext(){}

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                new EventLogger().WriteLog(ex.Message);
            }
        }

        public DbSet<Station> Stations { get; set; }

        public DbSet<StationHistory> StationHistory { get; set; }

        public DbSet<ApiToken> ApiToken { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Station>().HasAlternateKey(c => new { c.Hostname, c.MacAddress }).HasName("IX_MultipleColumns");

            modelBuilder.Entity<Station>().HasMany(c => c.StationHistories).WithOne(c => c.Station).IsRequired();
        }
    }
}