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
        public DbSet<Hashtag> Hashtags { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Image> Images { get; set; }

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

            builder.Entity<Hashtag>(
                h =>
                {
                    h.HasKey(k => k.HashtagId);
                    h.Property(p => p.HashtagName).HasMaxLength(50);
                });

            builder.Entity<Gallery>(
                g =>
                {
                    g.HasKey(k => k.GalleryId);
                    g.Property(p => p.GalleryName).HasMaxLength(50).IsRequired();
                    g.Property(p => p.Description).HasMaxLength(600);
                });

            builder.Entity<Image>(
                i =>
                {
                    i.HasKey(k => k.ImageId);
                    i.Property(p => p.ImageName).HasMaxLength(50).IsRequired();
                    i.Property(p => p.CreateDate).HasDefaultValue(DateTime.Now);
                    i.HasOne(h => h.Gallery).WithMany("Images")
                        .HasForeignKey(f => f.GalleryId);
                });

            base.OnModelCreating(builder);
        }
    }

}
