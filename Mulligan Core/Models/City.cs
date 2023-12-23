using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulligan.Core.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public List<Address> AddressList { get; set; }        
    }
}
