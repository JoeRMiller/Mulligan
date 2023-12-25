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

namespace Mulligan.Command
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var courseId = 3938;
            var facilityID = 3723;
            var ncrdb = new NCRDB();
            List<NCRDBTee> tees = await ncrdb.GetTees(courseId, facilityID);

            var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
            var configuration = builder.Build();
            string connectionString = configuration["ConnectionStrings:MulliganDBConnectionString"];
            var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder.UseNpgsql(connectionString);
            using var dbContext = new CoreDbContext(optionsBuilder.Options);

            foreach (var tee in tees) 
            {
                TeeSet set = new();
                set.CourseId = courseId;
                set.Name = tee.TeeName;
                set.Gender = tee.Gender;
                set.Par = tee.Par;
                set.CourseRating = tee.CourseRating;
                set.BogeyRating = tee.BogeyRating;
                set.Slope = tee.SlopeRating;
                set.FrontRating = tee.FrontRating;
                set.FrontSlope = tee.FrontSlope;
                set.BackRating = tee.BackRating;
                set.BackSlope = tee.BackSlope;
                dbContext.Tees.Add(set);
            }

            dbContext.SaveChanges();

            //var facilitator = new Facilitator();
            //var lines = File.ReadAllLines(@"..\..\..\states.csv");
            //foreach (var line in lines)
            //{
            //    var splits = line.Split(',');
            //    var stateCode = splits[1];
            //    await facilitator.LoadStateCourses(stateCode);
            //    //Console.WriteLine(facilitator.Facilities);
            //    //var facilities = JsonConvert.DeserializeObject<List<FacilityJson>>(facilitator.Facilities);
            //    var facilities = JsonConvert.DeserializeObject<List<FacilityRecord>>(facilitator.Facilities);
            //    var importer = new ImportFacility();
            //    importer.ImportFacilities(facilities);
            //    Console.WriteLine($"Imported Staate: {stateCode}");
            //}

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