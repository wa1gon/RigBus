using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Models
{
    
    public class DummyRig : RigBase
    {
        private const string name = "Dummy";


        internal DummyRig()
        {
            RigConf = new RigConfig();
            RigConf.RigName = name;
            RigConf.RigType = name;
            RigConf.CommPort = "COM01";
            RigConf.Frequency = 14.000;
            RigConf.Mode = "CW";
            RigConf.Parity = "even";
            RigConf.StopBits = 1;
            RigConf.Bps = 19200;
            RigConf.DataBits = 1;
            RigConf.Cts = true;
            RigConf.Rts = true;
        }
    }
}
