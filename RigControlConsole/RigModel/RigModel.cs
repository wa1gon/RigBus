using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RigModel
    {
        public string RigName { get; set; }
        public string RigType { get; set; }
        public int Bps { get; set; }
        public int StopBits { get; set; }
        public string Parity { get; set; }
        public double Frequency { get; set; }
        public string Mode { get; set; }
    }
}
