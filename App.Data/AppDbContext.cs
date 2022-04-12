using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<Browser> Browsers { get; set; }
        public DbSet<Plataform> Plataforms { get; set; }
        public DbSet<UrlMetric> UrlMetrics { get; set; }
        public DbSet<UrlView> UrlView { get; set; }
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            new SetupTables(modelBuilder);

        }
    }

}
