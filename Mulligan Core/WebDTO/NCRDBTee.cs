using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulligan.Core.WebDTO
{
    public class NCRDBTee
    {
        public string TeeName { get; set; }
        public string Gender { get; set; }
        public int Par {  get; set; }
        public decimal CourseRating { get; set; }
        public decimal BogeyRating { get; set; }
        public int SlopeRating { get; set; }
        public decimal FrontRating { get; set; }
        public int FrontSlope { get; set; }
        public decimal BackRating { get; set; }
        public int BackSlope { get; set; }

        public NCRDBTee(string teeName, string gender, int par, decimal courseRating, decimal bogeyRating, int slopeRating, decimal frontRating, int frontSlope, decimal backRating, int backSlope)
        {
            TeeName = teeName;
            Gender = gender;
            Par = par;
            CourseRating = courseRating;
            BogeyRating = bogeyRating;
            SlopeRating = slopeRating;
            FrontRating = frontRating;
            FrontSlope = frontSlope;
            BackRating = backRating;
            BackSlope = backSlope;
        }
    }
}
