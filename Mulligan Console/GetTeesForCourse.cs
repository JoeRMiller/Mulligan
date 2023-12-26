using Mulligan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mulligan.Core.Data;
using Mulligan.Core.WebData;
using Mulligan.Core.WebDTO;
using Microsoft.EntityFrameworkCore;

namespace Mulligan.Command
{
    public class GetTeesForCourse
    {
        private CoreDbContext _dbContext;

        public GetTeesForCourse(CoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TeeSet>> GetTeeSetAsync(int courseId)
        {
            var existing = _dbContext.Tees.Where(t => t.CourseId == courseId).ToList();
            if (existing.Count > 0)
            {
                Console.WriteLine("Found Existing");
                return existing;
            }

            
            var ncrdb = new NCRDB();
            List<NCRDBTee> tees = await ncrdb.GetTees(courseId);

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
                _dbContext.Tees.Add(set);
            }

            _dbContext.SaveChanges();
            return _dbContext.Tees.Where(t => t.CourseId == courseId).ToList();
        }
    }
}
