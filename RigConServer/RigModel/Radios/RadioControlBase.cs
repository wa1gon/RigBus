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
using Wa1gon.Models.Common;

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
        virtual public RadioPropComandList ReadSettings()
        {
            return null;
        }

        virtual public void GetSettings(RadioPropComandList cmd)
        {
            foreach (var item in cmd.Settings)
            {
                switch (item.PropertyName.ToLower())
                {
                    case RadioConstants.Mode:
                        GetMode(item);
                        break;
                    case RadioConstants.Freq:
                        GetFreq(item);
                        break;
                }
            }
        }
        virtual public void SetSettings(RadioPropComandList cmd)
        {
            foreach (var item in cmd.Settings)
            {
                switch (item.PropertyName.ToLower())
                {
                    case RadioConstants.Mode:
                        SetMode(item);
                        break;
                    case RadioConstants.Freq:
                        SetFreq(item);
                        break;
                }
            }
        }
        public virtual void GetMode(RadioProperty item)
        {
            item.Status = NotSupported;
        }
        public virtual void GetFreq(RadioProperty item)
        {
            item.Status = NotSupported;
        }
        public virtual void SetMode(Common.RadioProperty item)
        {
            item.Status = NotSupported;
        }
        public virtual void SetFreq(Common.RadioProperty item)
        {
            item.Status = NotSupported;
        }

        public const string NotSupported = "Not Supported";
    }
}
