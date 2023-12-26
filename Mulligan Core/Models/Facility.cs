using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulligan.Core.Models
{
    public class Facility
    {
        //private int _id;
        private int _NCRDId;
        private string _name;
        private string _address1;
        private string? _address2;
        private string _city;
        private string _state;
        private string _country;
        private int _entCountryCode;
        private int _entStateCode;
        private string? _phoneNumber;
        private string? _email;
        private List<Course> _courses;

        public Facility()
        {
            _courses = [];
        }
        
        public Facility(int ncrdId, string name, string address1, string address2, string city, string state, string country, int entCountryCode, int entStateCode, string phoneNumber, string email)
        {
            _NCRDId = ncrdId;
            _name = name;
            _address1 = address1;
            _address2 = address2;
            _city = city;
            _state = state;
            _country = country;
            _entCountryCode = entCountryCode;
            _entStateCode = entStateCode;
            _phoneNumber = phoneNumber;
            _email = email;
            _courses = [];
        }

        //public int Id { get => _id; set => _id = value; }
        public int NCRDId { get => _NCRDId; set => _NCRDId = value; }
        public string Name { get => _name; set => _name = value; }
        public string Address1 { get => _address1; set => _address1 = value; }
        public string? Address2 { get => _address2; set => _address2 = value; }
        public string City { get => _city; set => _city = value; }
        public string State { get => _state; set => _state = value; }
        public string Country { get => _country; set => _country = value; }
        public int EntCountryCode { get => _entCountryCode; set => _entCountryCode = value; }
        public int EntStateCode { get => _entStateCode; set => _entStateCode = value; }
        public string? PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string? Email { get => _email; set => _email = value; }
        public List<Course> Courses { get => _courses; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Facility: {NCRDId} - {State} {Name}");
            return sb.ToString();
        }
    }
}
