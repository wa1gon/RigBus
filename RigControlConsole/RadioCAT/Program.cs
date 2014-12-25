using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Models;
using System.Threading;

namespace RadioCAT
{
    class Program
    {
        //SerialPort serialPort1;
        public  FlexMaster master {get;set;}
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.master = (FlexMaster) RadioFactory.Get("flex");
            prg.master.Config.Port = "COM4";
            prg.master.Config.Bps = 19200;
            prg.master.OpenPort();
            RigSettings settings;

            while (true)
            {
                settings = prg.getFlexStatus();
                Console.WriteLine("VFO A: {0}", settings.Vfo_AFreq);
                Console.WriteLine("VFO B: {0}", settings.Vfo_BFreq);
                Console.WriteLine("Mode: {0}", settings.Rx1Mode);
                Thread.Sleep(1000);
            }
            //prg.SendKeyboard();


        }

        private RigSettings getFlexStatus()
        {
            RigSettings settings;
            settings = master.RequestStatus();
            return settings;
        }

        //private void OpenPort()
        //{

        //    master.Config.Port = "COM4";
        //    master.Config.Bps = 19200;
        //    master.OpenPort();
        //    this.serialPort1.DataReceived += new SerialDataReceivedEventHandler(
        //        this.serialPort1_DataReceived);

        //    serialPort1.Open();
        //}
        //private void SendKeyboard()
        //{
        //    char[] buff = new char[1];
        //    if (serialPort1.IsOpen)
        //    {
        //        while (true)
        //        {
        //            var key = Console.ReadKey();
        //            buff[0] = key.KeyChar;
        //            serialPort1.Write(buff, 0, 1);
        //        }

        //    }
        //}
        //string rxString;
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            //rxString = serialPort1.ReadExisting();
            //Console.WriteLine();
            //Console.WriteLine(rxString);
        }

    }
}
