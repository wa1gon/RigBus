using System;
namespace Models
{
    public interface IRigModel
    {
        int Bps { get; set; }
        string CommPort { get; set; }
        double Frequency { get; set; }
        string Mode { get; set; }
        string Parity { get; set; }
        string RigName { get; set; }
        string RigType { get; set; }
        int StopBits { get; set; }
        int DataBits { get; set; }
        bool Rts { get; set; }
        bool Cts { get; set; }
    }
}
