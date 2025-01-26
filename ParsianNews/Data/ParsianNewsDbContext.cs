using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParsianNews.Models;

namespace ParsianNews.Data
{
    public class ParsianNewsDbContext : IdentityDbContext
    {
        public ParsianNewsDbContext(DbContextOptions<ParsianNewsDbContext> options) : base(options)
        {
        }

        #region DbSets

        public DbSet<ReportGroup> ReportGroups { get; set; }
        public DbSet<Report> Reports { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ReportGroup>(
                r =>
                {
                    r.HasKey(h => h.GroupId);
                    r.Property(p => p.GroupName).IsRequired().HasMaxLength(100);
                    r.Property(p => p.GroupImage).HasMaxLength(50);
                });

            builder.Entity<Report>(
                r =>
                {
                    r.HasKey(h => h.ReportId);
                    r.Property(p => p.Title).IsRequired().HasMaxLength(100);
                    r.Property(p => p.Description).HasMaxLength(1000);
                    r.Property(p => p.FullText);
                    r.Property(p => p.ReportImage).HasMaxLength(100);
                    r.Property(p => p.ReportImageAlt).HasMaxLength(100);
                    r.Property(p => p.ImageTitle).HasMaxLength(100);
                    r.Property(p => p.ReportSource).HasMaxLength(100);
                    r.Property(p => p.Tags).HasMaxLength(500);
                    r.Property(p => p.CreateDate);
                    r.Property(p => p.Author).HasMaxLength(100);
                    r.Property(p => p.ReportView);
                    r.Property(p => p.IsHotNews);
                    r.Property(p => p.HotNewsDate);
                    r.HasOne(h => h.ReportGroup).WithMany("Reports")
                        .HasForeignKey(f => f.ReportGroupId);
                });

            base.OnModelCreating(builder);
        }
    }

}
