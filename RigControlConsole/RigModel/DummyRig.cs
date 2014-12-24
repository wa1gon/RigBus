using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    
    public class DummyRig : IRigModel
    {
        private const string name = "Dummy";
        public int Bps { get; set; }
        public string CommPort { get; set; }
        public double Frequency { get; set; }
        public string Mode { get; set; }
        public string Parity { get; set; }
        public string RigName { get; set; }
        public string RigType { get; set; }
        public int StopBits { get; set; }
        public int DataBits { get; set; }
        public bool Rts { get; set; }
        public bool Cts { get; set; }

        internal DummyRig()
        {
            RigName = name;
            RigType = name;
            CommPort = "COM01";
            Frequency = 14.000;
            Mode = "CW";
            Parity = "even";
            StopBits = 1;
            Bps = 19200;
            DataBits = 1;
            Cts = true;
            Rts = true;
        }
    }
}
