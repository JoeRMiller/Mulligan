using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Mulligan.Core.Data;
using Mulligan.Core.Models;
using Newtonsoft.Json;
using System;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Net;
using System.Text.RegularExpressions;
using Mulligan.Core.WebData;
using Mulligan.Core.WebDTO;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Mulligan.Command
{
    public class Program
    {
        
        static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
            var configuration = builder.Build();
            string connectionString = configuration["ConnectionStrings:MulliganDBConnectionString"];
            var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            using var dbContext = new CoreDbContext(optionsBuilder.Options);
            var search = new Search();

            var blue = new BlueGolf();
            await blue.FetchScoreCard("La Costa Resort & Spa - CHAMPIONS");

            int courseId = 23865;
            //await search.GetTees(courseId, dbContext);
            
            string course = "Oceanside";
            
            if (args[0] == "--search")
            {
                course = args[1];
                await search.SearchFacility(course, dbContext);
            }
            else if (args[0] == "--course")
            {
                courseId = int.Parse(args[1]);
                await search.GetTees(courseId, dbContext);
            }
        }
    }
}

/*
// Set up dependency injection
            var serviceProvider = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConfiguration(configuration.GetSection("Logging"));
                    builder.AddConsole();
                })
                .AddSingleton<HttpClient>()
                .BuildServiceProvider();
*/