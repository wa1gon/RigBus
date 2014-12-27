using System;
using System.Collections.Generic;
namespace Models
{
    public class RigConfig
    {
        public RigConfig()
        {
            AdditionSetting = new Dictionary<string, string>();
        }
        public string RigName { get; set; }
        public string RigType { get; set; }
        public int Bps { get; set; }
        public string Port { get; set; }

        public string Parity { get; set; }
        public int StopBits { get; set; }
        public int DataBits { get; set; }
        public bool Rts { get; set; }
        public bool Cts { get; set; }
        public bool Default { get; set; }
        public bool IsConnected { get; set; }
        public string Command { get; set; }
        public Dictionary<string,string> AdditionSetting { get; set; }

        public string Status { get; set; }
    }
    
}
