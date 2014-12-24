using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Models
{
    public class Rig
    {
        public RigSettings Settings { get; set; }
        public RigConfig Config { get; set; }
        public SerialPort Port { get; set; }
    }
}
