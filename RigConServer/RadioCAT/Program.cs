using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using Wa1gon.Models;
using System.Threading;

namespace RadioCAT
{
    class Program
    {
        //SerialPort serialPort1;
        public  PowerSDRMaster master {get;set;}
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.master = (PowerSDRMaster) RadioFactory.Get("flex");
            prg.master.Config.Port = "COM4";
            prg.master.Config.Bps = 19200;
            prg.master.OpenPort();
            //RigSettings settings;

            while (true)
            {
                //settings = prg.GetMasterStatus();
                //Console.WriteLine("VFO A: {0}", settings.Vfo_AFreq);
                //Console.WriteLine("VFO B: {0}", settings.Vfo_BFreq);
                //Console.WriteLine("Mode: {0}", settings.Rx1Mode);
                Thread.Sleep(1000);
            }
            //prg.SendKeyboard();


        }

        //private RigSettings GetMasterStatus()
        //{
        //    RigSettings settings;
        //    settings = master.ReadSettings();
        //    return settings;
        //}


    }
}
