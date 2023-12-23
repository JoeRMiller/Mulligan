using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulligan.Core.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int AddressNumber {  get; set; }
        public int CityId {  get; set; }
        public City City { get; set; }
    }
}
