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

namespace Mulligan.Command
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            var ncrdb = new NCRDB();
            List<NCRDBTee> tees = await ncrdb.GetTees(3938, 3723);
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
            foreach (var tee in  tees)
            {
                Console.WriteLine($"Tee Name: {tee.TeeName}");
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