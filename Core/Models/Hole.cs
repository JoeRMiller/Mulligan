using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mulligan.Core.Models
{
    public class Hole
    {
        private int _id;
        private int _teeSetId;
        private int _number;
        private int _hdcp;
        private int _par;
        private int _yardarge;
        private TeeSet _teeSet;

        public Hole() 
        {

        }

        public int Id { get => _id; set => _id = value; }
        public int TeeSetId { get => _teeSetId; set => _teeSetId = value; }
        public int Number { get => _number; set => _number = value; }
        public int Hdcp { get => _hdcp; set => _hdcp = value; }
        public int Par { get => _par; set => _par = value; }
        public int Yardarge { get => _yardarge; set => _yardarge = value; }
        public TeeSet TeeSet { get => _teeSet; set => _teeSet = value; }
    }
}
