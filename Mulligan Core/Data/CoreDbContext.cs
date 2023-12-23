using Microsoft.EntityFrameworkCore;
using Mulligan.Core.Models;
using System.Diagnostics;

namespace Mulligan.Core.Data
{
    public class CoreDbContext : DbContext
    {
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.HasSequence<int>("StateSequence").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("CitySequence").StartsAt(1).IncrementsBy(1);
            modelBuilder.HasSequence<int>("AddressSequence").StartsAt(1).IncrementsBy(1);

            var state = modelBuilder.Entity<State>();
            state.HasKey(s => s.Id);
            state.Property(s => s.Id).HasDefaultValueSql("nextval('\"StateSequence\"')");
            state.Property(s => s.Id).IsRequired();
            state.Property(s => s.Name).IsRequired();
            state.Property(s => s.Initials).IsRequired();


            var city = modelBuilder.Entity<City>();
            city.HasKey(c => c.Id);
            city.Property(c => c.Id).HasDefaultValueSql("nextval('\"CitySequence\"')");
            city.Property(c => c.Id).IsRequired();
            city.Property(c => c.StateId).IsRequired();
            city.Property(c => c.Name).IsRequired();
            city.HasOne(c => c.State)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.StateId);

            var address = modelBuilder.Entity<Address>();
            address.HasKey(a => a.Id);
            address.Property(a => a.Id).HasDefaultValueSql("nextval('\"AddressSequence\"')");
            address.Property(a => a.Id).IsRequired();
            address.Property(a => a.CityId).IsRequired();
            address.Property(a => a.AddressNumber).IsRequired();
            address.Property(a => a.StreetName).IsRequired();
            address.HasOne(a => a.City)
                .WithMany(c => c.AddressList)
                .HasForeignKey(a => a.CityId);
                
            




        }
    }
}
