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

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using Wa1gon.Models.Common;

namespace Wa1gon.Models
{
    public class PowerSDRMaster : RadioControlBase
    {
        private string AtuOn = "1";
        private string results;
        private object lockObject;
        private char [] delimiter = {':'};
        private bool isPortOpen = false;
        private Dictionary<string, string> modeLookup = new Dictionary<string, string>();
        private Dictionary<string, string> pmodeLookup = new Dictionary<string, string>();
        internal PowerSDRMaster()
        {
            Port = new SerialPort();
            lockObject = new object();
            CreateModeLookupDictionary();
        }

        private void CreateModeLookupDictionary()
        {
            modeLookup["00"] = RadioConstants.LSB;
            modeLookup["01"] = RadioConstants.USB;
            modeLookup["02"] = RadioConstants.DSB;
            modeLookup["03"] = RadioConstants.CWL;
            modeLookup["04"] = RadioConstants.CWU;
            modeLookup["05"] = RadioConstants.FM;
            modeLookup["06"] = RadioConstants.AM;
            modeLookup["07"] = RadioConstants.DIGU;
            modeLookup["08"] = RadioConstants.SPEC;
            modeLookup["09"] = RadioConstants.DIGL;
            modeLookup["10"] = RadioConstants.SAM;
            modeLookup["06"] = RadioConstants.DRM;

            pmodeLookup[RadioConstants.LSB] = "00";
            pmodeLookup[RadioConstants.USB] = "01";
            pmodeLookup[RadioConstants.DSB] = "02";
            pmodeLookup[RadioConstants.CWL] = "03";
            pmodeLookup[RadioConstants.CWU] = "04";
            pmodeLookup[RadioConstants.FM] = "05";
            pmodeLookup[RadioConstants.AM] = "06";
            pmodeLookup[RadioConstants.DIGU] = "07";
            pmodeLookup[RadioConstants.SPEC] = "08";
            pmodeLookup[RadioConstants.DIGL] = "09";
            pmodeLookup[RadioConstants.SAM] = "10";
            pmodeLookup[RadioConstants.DRM] = "06";
        }

        public void OpenPort()
        {
            if (isPortOpen == false)
            {
                Port = new SerialPort();
                Port.ReadTimeout = 300;
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
                isPortOpen = true;
            }
            string radioData;
            radioData = ReadToSemiFromCom();
            if (string.IsNullOrWhiteSpace(radioData))
            {
                return;
            }
            HandleAsyncRadioMessage(radioData);
        }

        private void HandleAsyncRadioMessage(string radioData)
        {
            Console.WriteLine("Received radio status: {0}", radioData);
        }
        override public RadioPropComandList ReadSettings()
        {
            Port.Write("ZZDU;");
            results = ReadToSemiFromCom();
            RadioPropComandList rc;
            lock (lockObject)
            {
                rc = null;
            }
            return rc;
        }
        public override void GetFreq(RadioProperty item)
        {
            string freq;
            try
            {
                OpenPort();
            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string vfoCmd;
                if (item.EnumItemNum.ToLower() == "a")
                {
                    vfoCmd = "ZZFA";
                }
                else
                {
                    vfoCmd = "ZZFB";
                }

                freq = FormatFreq(item.PropertyValue);
                string rCmd = string.Format("{0};", vfoCmd);
                Port.Write(rCmd);

                string resp = ReadRadioComm(item);
                if (string.IsNullOrWhiteSpace(resp)) return;

                double nfreq = double.Parse(resp.Substring(4)) / 1000000;
                item.PropertyValue = nfreq.ToString();

            }
            catch (Exception e)
            {
                item.Status = e.Message;

            }
        }
        public override void SetFreq(RadioProperty item)
        {
            string freq;
            try
            {
                OpenPort();
            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string vfoCmd;
                if (item.EnumItemNum.ToLower() == "a")
                {
                    vfoCmd = "ZZFA";
                }
                else
                {
                    vfoCmd = "ZZFB";
                }

                freq = FormatFreq(item.PropertyValue);
                string rCmd = string.Format("{0}{1};", vfoCmd, freq);
                Port.Write(rCmd);

                // expecting timeout
                ReadRadioComm(item);
            }
            catch (Exception e)
            {
                item.Status = e.Message;
            }
        }

        private string FormatFreq(string p)
        {
           try
           {
               double freq = double.Parse(p) * 1000000.0;
               StringBuilder sFreq = new StringBuilder(freq.ToString());
               while(sFreq.Length < 11)
               {
                   sFreq.Insert(0, "0");
               }
               return sFreq.ToString();
           }
            catch(Exception)
           {
               return "ERROR";
           }
        }
        public override void GetMode(Common.RadioProperty item)
        {
            string pmode;
            try
            {

                OpenPort();

            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string vfoCmd;
                if (item.EnumItemNum.ToLower() == "a")
                {
                    vfoCmd = "ZZMD";
                }
                else
                {
                    vfoCmd = "ZZME";
                }

                string rCmd = string.Format("{0};", vfoCmd);
                Port.Write(rCmd);

                string resp = ReadRadioComm(item);
                if (string.IsNullOrWhiteSpace(resp))
                {
                    item.PropertyValue = null;
                    item.Status = "No data on comm port.";
                    return;
                }

                pmode = resp.Substring(4);
                item.PropertyValue = modeLookup[pmode];

            }
            catch (Exception e)
            {
                item.Status = e.Message;

            }
        }
        public override void SetAG(RadioProperty item)
        {

            try
            {
                OpenPort();
            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string cmd;
                item.Status = RadioConstants.Ok;

                cmd = "ZZAG";

                double agValue = Convert.ToDouble( agValue = int.Parse(item.PropertyValue));

                agValue = (agValue / 100) * 255;

                int agInt = Convert.ToInt32(agValue + .5);
                string rCmd = string.Format("{0}0{1};", cmd, agInt.ToString("D3"));
                Port.Write(rCmd);

                // expecting timeout  		

                string resp = ReadRadioComm(item);
                item.Status = RadioConstants.Ok;

            }
            catch (Exception e)
            {
                item.Status = e.Message;

            }
        }
        public override void GetAG(RadioProperty item)
        {

            try
            {
                OpenPort();
            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string cmd;
                item.Status = RadioConstants.Ok;

                cmd = "ZZAG";

                string rCmd = string.Format("{0};", cmd);
                Port.Write(rCmd);		

                string resp = ReadRadioComm(item);
                if (string.IsNullOrWhiteSpace(resp))
                {
                    item.PropertyValue = null;
                    item.Status = "No data on comm port.";
                    return;
                }

                string agString = resp.Substring(4);

                int agInt = Convert.ToInt32(agString);
                item.PropertyValue = agInt.ToString();
                item.Status = RadioConstants.Ok;

            }
            catch (Exception e)
            {
                item.Status = e.Message;
            }
        }
        public override void SetAtuButton(RadioProperty item)
        {

            try
            {

                OpenPort();

            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string cmd;
                item.Status = RadioConstants.Ok;

                cmd = "ZZOW";

                string rCmd = string.Format("{0}{1};", cmd, item.PropertyValue);
                Port.Write(rCmd);

                // expecting timeout  		

                string resp = ReadRadioComm(item);

            }
            catch (Exception e)
            {
                item.Status = e.Message;

            }
        }

        private string ReadRadioComm(RadioProperty item)
        {
            string resp = string.Empty;
            try
            {
                item.Status = RadioConstants.Ok;
                resp = ReadToSemiFromCom();
                if (resp == "?;")
                {
                    item.Status = "Radio Error ?";
                    return string.Empty;
                }
                //"ZZEM:ZZOW1:Feature Not Available"

                if (resp.Substring(0, 4) == "ZZEM")
                {
                    string[] errorArray = resp.Split(new char[] { ':' });
                    item.Status = errorArray[2];
                    return string.Empty;

                }

            }
            catch (Exception)
            { }
            return resp;
        }
        public override void GetAtuButton(RadioProperty item)
        {
            try
            {

                OpenPort();

            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string cmd;

                cmd = "ZZOW";

                string rCmd = string.Format("{0};", cmd);
                Port.Write(rCmd);

                // expecting timeout
                ReadRadioComm(item);
            }
            catch (Exception e)
            {
                item.Status = e.Message;

            }
        }
        public override void SetVerboseError(Common.RadioProperty item)
        {
            try
            {

                OpenPort();

            }
            catch (Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {


                string rCmd = string.Format("ZZEM{0};",item.PropertyValue);
                Port.Write(rCmd);

                // expecting timeout
                ReadRadioComm(item);
            }
            catch (Exception e)
            {
                item.Status = e.Message;

            }
        }
        public override void SetMode(Common.RadioProperty item)
        {
            string pmode;
            try
            {

                OpenPort();
                
            } catch(Exception e)
            {
                item.Status = "Server Error: " + e.Message;
                return;
            }
            try
            {
                string vfoCmd;
                if (item.EnumItemNum.ToLower() == "a")
                {
                    vfoCmd = "ZZMD";
                }
                else
                {
                    vfoCmd = "ZZME";
                }

                pmode = pmodeLookup[item.PropertyValue];
                string rCmd = string.Format("{0}{1}{2};", vfoCmd,pmode[0], pmode[1]);
                Port.Write(rCmd);

                string resp = ReadRadioComm(item);
                if (string.IsNullOrWhiteSpace(resp))
                {
                    Console.WriteLine(resp);
                }
                item.Status = RadioConstants.Ok;
            }
            catch (Exception e)
            {
                item.Status = e.Message;

            }
        }
        /// <summary> Read up to and including the semicolon.
        /// </summary>
        /// <returns>Next response or empty if nothing was waiting</returns>
        private string ReadToSemiFromCom()
        {
            char inp;
            StringBuilder ret = new StringBuilder();
            try
            {
                while (true)
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
            catch (TimeoutException)
            {
                if (ret.Length == 0)
                {
                    return string.Empty;
                }

                return ret.ToString();
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
    }
}
