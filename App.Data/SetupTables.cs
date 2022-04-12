using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Data
{
    public class SetupTables
    {
        private readonly ModelBuilder mb;

        public SetupTables(ModelBuilder modelBuilder)
        {

            this.mb = modelBuilder;
            SetupUrl();
            SetupBrowser();
            SetupPlataform();
            SetupMetric();
            SetupUrlView();
        }

        private void SetupUrlView()
        {
            mb.Entity<UrlView>(e => { 
                e.ToView("UrlView"); 
                e.HasNoKey(); }
            );
        }

        private void SetupMetric()
        {
            mb.Entity<UrlMetric>(e => e.ToTable("UrlMetric"));
            mb.Entity<UrlMetric>().Property(e => e.Id).UseIdentityColumn(1, 1);
            mb.Entity<UrlMetric>().HasKey(e => e.Id);
            mb.Entity<UrlMetric>().HasIndex(e => e.BrowserId);
            mb.Entity<UrlMetric>().HasIndex(e => e.PlataformId);
            mb.Entity<UrlMetric>().HasIndex(e => e.UrlId);
        }

        private void SetupPlataform()
        {
            mb.Entity<Plataform>(e => e.ToTable("Plataform"));
            mb.Entity<Plataform>().Property(e => e.Id).UseIdentityColumn(1, 1);
            mb.Entity<Plataform>().HasKey(e => e.Id);

            mb.Entity<Plataform>().Property(e => e.Name).HasMaxLength(20).IsRequired();
            mb.Entity<Plataform>().HasIndex(e => e.Name).IsUnique();
        }

        private void SetupBrowser()
        {
            mb.Entity<Browser>(e => e.ToTable("Browser"));
            mb.Entity<Browser>().Property(e => e.Id).UseIdentityColumn(1, 1);
            mb.Entity<Browser>().HasKey(e => e.Id);
            mb.Entity<Browser>().Property(e => e.Name).HasMaxLength(20).IsRequired();
            mb.Entity<Browser>().HasIndex(e => e.Name).IsUnique();
        }

        private void SetupUrl()
        {
            mb.Entity<Url>(e => e.ToTable("Url"));
            mb.Entity<Url>().HasKey(e => e.Id);
            mb.Entity<Url>().Ignore(e => e.Count);
            mb.Entity<Url>().Property(e => e.Id).HasDefaultValueSql("NEWID()");
            mb.Entity<Url>().Property(e => e.ShortUrl).HasMaxLength(5).IsRequired();
            mb.Entity<Url>().Property(e => e.OriginalUrl).HasMaxLength(500).IsRequired();
            mb.Entity<Url>().HasIndex(e => e.ShortUrl).IsUnique();
            mb.Entity<Url>().HasIndex(e => e.OriginalUrl).IsUnique();
        }
    }

}
