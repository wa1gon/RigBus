using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class FlexMaster : MasterBase
    {
        private string results;
        private object lockObject;
        private char [] delimiter = {':'};
        private Dictionary<string, string> modeLookup = new Dictionary<string, string>();
        internal FlexMaster()
        {
            Port = new SerialPort();
            lockObject = new object();
            modeLookup["00"] = "LSB";
            modeLookup["01"] = "USB";
            modeLookup["02"] = "DSB";
            modeLookup["03"] = "CWL";
            modeLookup["04"] = "CWU";
            modeLookup["05"] = "FM";
            modeLookup["06"] = "AM";
            modeLookup["07"] = "DIGU";
            modeLookup["08"] = "SPEC";
            modeLookup["09"] = "DIGL";
            modeLookup["10"] = "SAM";
            modeLookup["06"] = "DRM";
        }

        public void OpenPort()
        {
            Port = new SerialPort();
            Port.PortName = Config.Port;
            Port.BaudRate = Config.Bps;
            Port.ReadTimeout = 200;
            //Port.DataReceived += new SerialDataReceivedEventHandler(serialDataReceived);
            Port.Open();

        }
        public RigSettings RequestStatus()
        {
            Port.Write("ZZDU;");
            results = ReadstatusFromPort();
            RigSettings rc;
            lock (lockObject)
            {
                rc=ParseStatus(results);
            }
            return rc;
        }

        private string ReadstatusFromPort()
        {
            char inp;
            StringBuilder ret = new StringBuilder();
            while(true)
            {
                inp = (char)Port.ReadChar();
                if (inp != ';')
                {
                    ret.Append(inp);
                }
                else
                {
                    return ret.ToString();
                }

            }
        }

        private RigSettings ParseStatus(string res)
        {
            var rc = new RigSettings();
            var settings = res.Split(delimiter);

            int i = 1;
            rc.VfoABButton = settings[i++]; //1
            rc.VfoSplit = settings[i++];    //2
            rc.TunButton = settings[i++];   //3
            rc.MoxButton = settings[i++];   //4
            rc.Rx1Ant = settings[i++];      //5
            rc.TxAnt = settings[i++];       //7
            rc.Rx2Enable = settings[i++];   //8
            rc.RitEnable = settings[i++];   //9

            rc.DisplayMode = settings[i++];     //P10

            rc.AgcSelect = settings[i++];       //1
            rc.MultiRxEnable = settings[i++];   //2
            rc.XitEnable = settings[i++];       //3
            rc.StepSize = settings[i++];        //4
            rc.Rx1Mode = modeLookup[ settings[i++]];         //5
            rc.Rx2Mode = modeLookup[settings[i++]];         //6
            rc.Rx2DspFilter = settings[i++];    //7
            rc.Rx1DspFilter = settings[i++];    //8
            rc.TxRelays = settings[i++];        //9
            rc.Rx2Band = settings[i++];         //P20

            rc.DriveLevel = settings[i++];      //1
            rc.Rx1Band = settings[i++];         //2
            rc.AudioGain = settings[i++];       //3
            rc.CWSpeed = settings[i++];         //4
            rc.TunePower = settings[i++];       //5
            rc.PrimaryDCVolts = settings[i++];  //6
            rc.SMeterLevel = settings[i++];     //7
            rc.RitFreq = settings[i++];         //8
            rc.TempSensor = settings[i++];      //9
            rc.XitFreq = settings[i++];         //P30
            rc.CPUUsage = settings[i++];        //P31
            rc.Vfo_AFreq = settings[i++];       //P32
            rc.Vfo_BFreq = settings[i++];       //P33

            return rc;


        }
        private void serialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            lock (lockObject)
            {
                results = Port.ReadExisting();
                if (results.Length > 0)
                {

                }
            }
        }
    }
}
