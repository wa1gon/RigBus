using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Models;

namespace RadioCAT
{
    class Program
    {
        SerialPort serialPort1;
        public  FlexMaster master {get;set;}
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.OpenPort();
            RigSettings settings;

            prg.master = (FlexMaster)RadioFactory.Get("flex");
            while (true)
            {
                settings = getFlexStatus();              
            }
            //prg.SendKeyboard();


        }

        private static RigSettings getFlexStatus()
        {
            throw new NotImplementedException();
        }

        private void OpenPort()
        {

            master.Config.Port = "COM4";
            master.Config.Bps = 19200;
            master.OpenPort();
            this.serialPort1.DataReceived += new SerialDataReceivedEventHandler(
                this.serialPort1_DataReceived);

            serialPort1.Open();
        }
        private void SendKeyboard()
        {
            char[] buff = new char[1];
            if (serialPort1.IsOpen)
            {
                while (true)
                {
                    var key = Console.ReadKey();
                    buff[0] = key.KeyChar;
                    serialPort1.Write(buff, 0, 1);
                }

            }
        }
        string rxString;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            rxString = serialPort1.ReadExisting();
            //Console.WriteLine();
            //Console.WriteLine(rxString);
        }

    }
}
