using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mulligan.Core.Models;
using Mulligan.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Mulligan.CLI
{
    public struct FacilityJson
    {
        public int courseID;
        public string courseName;
        public int facilityID;
        public string facilityName;
        public string address1;
        public string? address2;
        public string city;
        public string state;
        public string country;
        public int entCountryCode;
        public int entStateCode;
        public string telephone;
        public string email;
    }
    public class ImportFacility
    {
        //public void ImportFacilities(List<FacilityJson> facilities)
        public void ImportFacilities(List<FacilityRecord> facilities)
        {
            var builder = new ConfigurationBuilder().AddUserSecrets<Program>();
            var configuration = builder.Build();
            string connectionString = configuration["ConnectionStrings:MulliganDBConnectionString"];
            //System.Console.WriteLine(connectionString);
            var optionsBuilder = new DbContextOptionsBuilder<CoreDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            using var dbContext = new CoreDbContext(optionsBuilder.Options);

            foreach (var f in facilities)
            {
                if (f.address1 != null)
                {
                    var state = f.state.Split('-')[1];
                    Facility facility = new Facility(f.facilityID, f.facilityName, f.address1, f.address2, f.city, state, f.country, f.entCountryCode, f.entStateCode, f.phoneNumber, f.email);
                    if (dbContext.Facilities.Find(facility.NCRDId) == null)
                    {
                        dbContext.Add<Facility>(facility);
                        //Console.WriteLine($"Adding Facility: {facility.State} {facility.NCRDId}, {facility.Name}");
                    }
                    Course course = new Course(f.courseID, f.courseName, f.facilityID, (int)CourseClass.Unknown);
                    dbContext.Add<Course>(course);
                }
            }
            //foreach (var f in dbContext.Facilities)
            //{
            //    Console.WriteLine($"{f.NCRDId}:{f.Name}");
            //}

            dbContext.SaveChanges();


        }
    }
}
