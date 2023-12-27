using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Identity.Client;
using Mulligan.Core.Data;
namespace Mulligan.Web.Data

{
    public class CoreDbContextFactory : IDesignTimeDbContextFactory<CoreDbContext>
    {
        public CoreDbContext CreateDbContext(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;
            var configuration = builder.Configuration;

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("MulliganDBConnectionString") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder.UseNpgsql(connectionString, x => x.MigrationsAssembly("Mulligan Core"));

            return new CoreDbContext(optionsBuilder.Options);
        }
    }
}
