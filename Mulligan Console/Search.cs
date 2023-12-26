using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mulligan.Core.Data;
using Mulligan.Core.WebData;
using Mulligan.Core.WebDTO;
using Mulligan.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Mulligan.Command
{
    public class Search
    {
        public async Task SearchFacility(string search, CoreDbContext dbContext)
        {
            var facilities = dbContext.Facilities.Where(f => f.Name.ToLower().Contains(search.ToLower())).ToList();
            foreach (var facility in facilities)
            {
                Console.WriteLine(facility);
                await SearchCoursesByFacility(facility, dbContext);
            }
        }

        public async Task SearchCoursesByFacility(Facility facility, CoreDbContext dbContext)
        {
            var courses = dbContext.Courses.Where(c => c.FacilityId == facility.NCRDId).ToList();
            foreach (var course in courses)
            {
                Console.WriteLine(course);
                await GetTees(course.NCRDId, dbContext);
            }
        }

        public async Task SearchCourse(string search, CoreDbContext dbContext)
        {
            var courses = dbContext.Courses.Where(f => f.Name.ToLower().Contains(search.ToLower())).ToList();
            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }
        }

        public async Task SearchCourse(int courseId, CoreDbContext dbContext)
        {
            var courses = dbContext.Courses.Where(c => c.NCRDId == courseId).ToList();
            foreach (var course in courses)
            {
                Console.WriteLine(course);
            }
        }

        public async Task GetTees(int courseId, CoreDbContext dbContext)
        {
            var ncrdb = new NCRDB();
            List<NCRDBTee> tees = await ncrdb.GetTees(courseId);
            var getTees = new GetTeesForCourse(dbContext);
            var courseTees = await getTees.GetTeeSetAsync(courseId);
            foreach (var teeSet in courseTees)
            {
                Console.WriteLine(teeSet);
                Console.WriteLine();
            }
        }
    }
}
