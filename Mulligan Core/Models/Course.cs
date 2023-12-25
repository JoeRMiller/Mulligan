using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulligan.Core.Models
{
    public enum CourseClass : int
    {
        Public = 1,
        Private = 2,
        Unknown = 0
    }
    public class Course
    {
        //private int _id;
        private int _NCRDId;
        private string _name;
        private int _classType;
        private CourseClass _classification;
        private int _facilityId;
        private Facility _facility;
        private List<TeeSet> _tees;
        

        public Course(int NCRDId, string Name, int facilityId, int classType) 
        { 
            _NCRDId = NCRDId;
            _name = Name;
            _facilityId = facilityId;
            _classType = classType;
            _classification = (CourseClass)_classType;
            _tees = [];
        }

        //public int Id { get => _id; set => _id = value; }
        public int NCRDId { get => _NCRDId; set => _NCRDId = value; }
        public string Name { get => _name; set => _name = value; }
        public CourseClass Classification { get => _classification; set => _classification = value; }
        public int FacilityId { get => _facilityId; set => _facilityId = value; }
        public Facility Facility { get => _facility; set => _facility = value; }
        public int ClassType { get => _classType; set => _classType = value; }
        public List<TeeSet> Tees { get => _tees; }
    }
}
