using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulligan.Core.Models
{
    public class TeeSet
    {
        private int _id;
        private int _courseId;
        private string _name;
        private string _gender;
        private int _par;
        private decimal _courseRating;
        private decimal _bogeyRating;
        private int _slope;
        private decimal _frontRating;
        private int _frontSlope;
        private decimal _backRating;
        private int _backSlope;
        private Course _course;
        private List<Hole> _holes;

        public TeeSet() { }

        public int Id { get => _id; set => _id = value; }
        public int CourseId { get => _courseId; set => _courseId = value; }
        public string Name { get => _name; set => _name = value; }
        public string Gender { get => _gender; set => _gender = value; }
        public int Par { get => _par; set => _par = value; }
        public decimal CourseRating { get => _courseRating; set => _courseRating = value; }
        public decimal BogeyRating { get => _bogeyRating; set => _bogeyRating = value; }
        public int Slope { get => _slope; set => _slope = value; }
        public decimal FrontRating { get => _frontRating; set => _frontRating = value; }
        public int FrontSlope { get => _frontSlope; set => _frontSlope = value; }
        public decimal BackRating { get => _backRating; set => _backRating = value; }
        public int BackSlope { get => _backSlope; set => _backSlope = value; }
        public Course Course { get => _course; set => _course = value; }
        public List<Hole> Holes { get => _holes; set => _holes = value; }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append("Tee Set\t");
            sb.Append($"Id:{Id}\n");
            sb.Append($"Name:{Name}\t");
            sb.Append($"Gender:{Gender}\t");
            sb.Append($"Par:{Par}\t");
            sb.Append($"Rating:{CourseRating}\t");
            sb.Append($"Slope:{Slope}");
            return sb.ToString();
        }
    }
}
