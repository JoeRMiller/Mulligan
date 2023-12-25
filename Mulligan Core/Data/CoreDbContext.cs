using Microsoft.EntityFrameworkCore;
using Mulligan.Core.Models;
using System.Diagnostics;

namespace Mulligan.Core.Data
{
    public class CoreDbContext : DbContext
    {
        //public DbSet<State> States { get; set; }
        //public DbSet<City> Cities { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Course> Courses { get; set; }

        

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasSequence<int>("StateSequence").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence<int>("CitySequence").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence<int>("AddressSequence").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence<int>("FacilitySequence").StartsAt(1).IncrementsBy(1);
            //modelBuilder.HasSequence<int>("CourseSequence").StartsAt(1).IncrementsBy(1);

            var facility = modelBuilder.Entity<Facility>();
            facility.HasKey(f => f.NCRDId);
            //facility.Property(f => f.Id).HasDefaultValueSql("nextval('\"FacilitySequence\"')");
            facility.Property(f => f.Name).IsRequired();
            facility.Property(f => f.NCRDId).IsRequired();
            facility.Property(f => f.Address1).IsRequired();
            facility.Property(f => f.City).IsRequired();
            facility.Property(f => f.State).IsRequired();
            facility.Property(f => f.Country).IsRequired();
            facility.Property(f => f.EntCountryCode).IsRequired();
            facility.Property(f => f.EntStateCode).IsRequired();

            var course = modelBuilder.Entity<Course>(); ;
            course.HasKey(c => c.NCRDId);
            //course.Property(c => c.Id).HasDefaultValueSql("nextval('\"CourseSequence\"')");
            course.Property(c => c.Name).IsRequired();
            course.Property(c => c.FacilityId).IsRequired();
            course.Property(c => c.ClassType).IsRequired();
            course.HasOne(c => c.Facility)
                  .WithMany(f => f.Courses)
                  .HasForeignKey(c => c.FacilityId);



            //var state = modelBuilder.Entity<State>();
            //state.HasKey(s => s.Id);
            //state.Property(s => s.Id).HasDefaultValueSql("nextval('\"StateSequence\"')");
            //state.Property(s => s.Id).IsRequired();
            //state.Property(s => s.Name).IsRequired();
            //state.Property(s => s.Initials).IsRequired();


            //var city = modelBuilder.Entity<City>();
            //city.HasKey(c => c.Id);
            //city.Property(c => c.Id).HasDefaultValueSql("nextval('\"CitySequence\"')");
            //city.Property(c => c.Id).IsRequired();
            //city.Property(c => c.StateId).IsRequired();
            //city.Property(c => c.Name).IsRequired();
            //city.HasOne(c => c.State)
            //    .WithMany(s => s.Cities)
            //    .HasForeignKey(c => c.StateId);

            //var address = modelBuilder.Entity<Address>();
            //address.HasKey(a => a.Id);
            //address.Property(a => a.Id).HasDefaultValueSql("nextval('\"AddressSequence\"')");
            //address.Property(a => a.Id).IsRequired();
            //address.Property(a => a.CityId).IsRequired();
            //address.Property(a => a.AddressNumber).IsRequired();
            //address.Property(a => a.StreetName).IsRequired();
            //address.HasOne(a => a.City)
            //    .WithMany(c => c.AddressList)
            //    .HasForeignKey(a => a.CityId);
        }
    }
}

