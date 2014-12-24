using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FlexMaster : Rig
    {
        internal FlexMaster()
        {
            Port = new SerialPort();
        }

        public void OpenPort()
        {
            Port = new SerialPort();
            Port.PortName = Config.Port;
            Port.BaudRate = Config.Bps;
            Port.DataReceived += new SerialDataReceivedEventHandler(serialDataReceived);
            Port.Open();

        }

        private void serialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
