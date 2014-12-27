using System.Collections.Generic;
using System.IO.Ports;

namespace RigControlConsole
{
    public class ServerInfo 
    {
#warning this should be a singleton
        public List<string> SupportedRadios { get; set; }
        public string [] CommPorts { get; set; }
        public ServerInfo()
        {
            SupportedRadios = new List<string>();
            CommPorts = SerialPort.GetPortNames();

#warning need better way to do this

            SupportedRadios.Add("PowerSDR");
            SupportedRadios.Add("Dummy");
            SupportedRadios.Add("ICom746");

        }
    }
}
