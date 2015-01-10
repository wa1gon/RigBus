#region -- Copyright
/*
   Copyright {2014} {Darryl Wagoner DE WA1GON}

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
#endregion

using System.Collections.Generic;
using System.IO.Ports;
using System.Text;

namespace Wa1gon.Models
{
    public class PowerSDRMaster : RadioControlBase
    {
        private string results;
        private object lockObject;
        private char [] delimiter = {':'};
        private Dictionary<string, string> modeLookup = new Dictionary<string, string>();
        internal PowerSDRMaster()
        {
            Port = new SerialPort();
            lockObject = new object();
            CreateModeLookupDictionary();
        }

        private void CreateModeLookupDictionary()
        {
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
            if (Config.Bps != null)
            {
            Port.BaudRate = (int)Config.Bps;
            }
            else
            {
                Port.BaudRate = 4800;
            }
            Port.ReadTimeout = 200;
            Port.Open();

        }
        override public RadioCmd ReadSettings()
        {
            Port.Write("ZZDU;");
            results = ReadstatusFromPort();
            RadioCmd rc;
            lock (lockObject)
            {
                rc = null;
            }
            return rc;
        }
        public override void SetSettings(RadioCmd setting)
        {
            throw new System.NotImplementedException();
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
