using System;
namespace Models
{
    public class RigConfig
    {
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
    }
}
