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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace Wa1gon.Models
{
    /// <summary> Common base class for all radios 
    /// </summary>
    public class RadioControlBase
    {
        public RadioControlBase()
        {
            Settings = new RigSettings();
            Config = new RadioComConnConfig();
        }
        public RigSettings Settings { get; set; }
        public RadioComConnConfig Config { get; set; }
        [JsonIgnore]
        public SerialPort Port { get; set; }
        virtual public RadioCmd ReadSettings()
        {
            return null;
        }

        virtual public void SetSettings(RadioCmd cmd)
        {
            foreach (var item in cmd.Settings)
            {
                switch (item.Setting.ToLower())
                {
                    case "mode":
                        SetMode(item);
                        break;
                }
            }
        }

        public virtual void SetMode(Common.SettingValue item)
        {
            item.Status = "Not Supported";
        }
    }
}
