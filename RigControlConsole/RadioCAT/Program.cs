using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioCAT
{
    class Program
    {
        private System.IO.Ports.SerialPort serialPort1;
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.ReadSerial();

        }
        private void ReadSerial()
        {
            serialPort1.PortName = "COM4";
            serialPort1.BaudRate = 19200;

            serialPort1.Open();
            if (serialPort1.IsOpen)
            {

            }
        }

    }
}
