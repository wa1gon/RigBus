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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa1gon.Models
{
    /// <summary> Radio Settings such as mode, frequency, etc

    /// </summary>
    public class RigSettings
    {
        public RigSettings()
        {
            AdditionSetting = new Dictionary<string, string>();
        }
        
        public enum StatusCodes {Success, RadioError};

        public StatusCodes Status { get; set; }
        public string RigName { get; set; }
        public string VfoABButton { get; set; }
        public string VfoSplit { get; set; }
        public string TunButton { get; set; }
        public string MoxButton { get; set; }
        public string Rx1Ant { get; set; }
        public string Rx2Ant { get; set; }
        public string TxAnt { get; set; }
        public string Rx2Enable { get; set; }
        public string RitEnable { get; set; }
        public string DisplayMode { get; set; }
        public string AgcSelect { get; set; }
        public string MultiRxEnable { get; set; }
        public string XitEnable { get; set; }
        public string StepSize { get; set; }
        public string Rx1Mode { get; set; }
        public string Rx2Mode { get; set; }
        public string Rx1DspFilter { get; set; }
        public string Rx2DspFilter { get; set; }
        public string TxRelays { get; set; }
        public string Rx1Band { get; set; }
        public string Rx2Band { get; set; }
        public string DriveLevel { get; set; }
        public string AudioGain { get; set; }
        public string CWSpeed { get; set; }
        public string TunePower { get; set; }
        public string PrimaryDCVolts { get; set; }
        public string SMeterLevel { get; set; }
        public string RitFreq { get; set; }
        public string TempSensor { get; set; }
        public string XitFreq { get; set; }
        public string CPUUsage { get; set; }
        public string Mode { get; set; }
        public string Vfo_AFreq { get; set; }
        public string Vfo_BFreq { get; set; }
        public Dictionary<string, string> AdditionSetting { get; set; }

    }
}
