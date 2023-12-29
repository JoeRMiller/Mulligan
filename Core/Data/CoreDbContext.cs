using Microsoft.EntityFrameworkCore;
using Mulligan.Core.Models;
using System.Diagnostics;

namespace Mulligan.Core.Data
{
    public class CoreDbContext : DbContext
    {
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<TeeSet> Tees { get; set; }
        public DbSet<Hole> Holes { get; set; }

        

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("TeeSetSequence").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("HoleSequence").StartsAt(1).IncrementsBy(1);

            var facility = modelBuilder.Entity<Facility>();
            facility.HasKey(f => f.NCRDId);
            facility.Property(f => f.Name).IsRequired();
            facility.Property(f => f.NCRDId).IsRequired();
            facility.Property(f => f.Address1).IsRequired();
            facility.Property(f => f.City).IsRequired();
            facility.Property(f => f.State).IsRequired();
            facility.Property(f => f.Country).IsRequired();
            facility.Property(f => f.EntCountryCode).IsRequired();
            facility.Property(f => f.EntStateCode).IsRequired();

            var course = modelBuilder.Entity<Course>();
            course.HasKey(c => c.NCRDId);
            course.Property(c => c.Name).IsRequired();
            course.Property(c => c.FacilityId).IsRequired();
            course.Property(c => c.ClassType).IsRequired();
            course.HasOne(c => c.Facility)
                  .WithMany(f => f.Courses)
                  .HasForeignKey(c => c.FacilityId);

            var teeSet = modelBuilder.Entity<TeeSet>();
            teeSet.HasKey(t => t.Id);
            teeSet.Property(t => t.Id).HasDefaultValueSql("nextval('\"TeeSetSequence\"')");
            teeSet.Property(t => t.CourseId).IsRequired();
            teeSet.Property(t => t.Gender).IsRequired();
            teeSet.HasIndex(t => new { t.CourseId, t.Name, t.Gender }).IsUnique();
            teeSet.Property(t => t.Par).IsRequired();
            teeSet.Property(t => t.CourseRating).IsRequired();
            teeSet.Property(t => t.Slope).IsRequired();
            teeSet.HasOne(t => t.Course)
                  .WithMany(c => c.Tees)
                  .HasForeignKey(t => t.CourseId);

            var hole = modelBuilder.Entity<Hole>();
            hole.HasKey(h => h.Id);
            hole.Property(h => h.Id).HasDefaultValueSql("nextval('\"HoleSequence\"')");
            hole.HasIndex(h => new { h.Id, h.TeeSetId, h.Number }).IsUnique();
            hole.Property(h => h.Hdcp).IsRequired();
            hole.Property(h => h.Number).IsRequired();
            hole.Property(h => h.Par).IsRequired();
            hole.Property(h => h.Yardarge).IsRequired();
            hole.HasOne(h => h.TeeSet)
                .WithMany(t => t.Holes)
                .HasForeignKey(h => h.TeeSetId);
        }
    }
}

