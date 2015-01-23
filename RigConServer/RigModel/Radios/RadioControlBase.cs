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
        /// <summary> Gets a list of properties as requested by the 
        /// RadioPropCommandList.
        /// </summary>
        /// <param name="cmd">Command list with the current values.</param>
        virtual public void GetSettings(RadioPropComandList cmd)
        {
            foreach (var item in cmd.Properties)
            {
                LogItem(item, "get");
                switch (item.PropertyName.ToLower())
                {
                    case RadioConstants.Mode:
                        GetMode(item);
                        break;
                    case RadioConstants.Freq:
                        GetFreq(item);
                        break;
                    case RadioConstants.ATUButton:
                        GetAtuButton(item);
                        break;
                    case RadioConstants.VerboseError:
                        GetAtuButton(item);
                        break;
                    case RadioConstants.AG:
                        GetAG(item);
                        break;
                    case RadioConstants.AGF:
                        GetAGF(item);
                        break;
                    default:
                        NotImplemented(item);
                        break;
                }
                if (item.Status == RadioConstants.Ok)
                {
                    cmd.Success++;
                }
                else
                {
                    cmd.Failed++;
                }
            }
            
        }

        public virtual void GetAG(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }

        private void LogItem(RadioProperty item,string action)
        {
            Console.WriteLine("{0} Property: {1} Value: {2}", action, item.PropertyName,
                item.PropertyValue);
        }

        virtual public void GetAtuButton(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }
        /// <summary> Takes a list of Radio Properties and sets them.
        /// 
        /// </summary>
        /// <param name="cmd">List of command</param>
        public virtual void SetSettings(RadioPropComandList cmd)
        {
            foreach (var item in cmd.Properties)
            {
                LogItem(item, "set");
                switch (item.PropertyName.ToLower())
                {
                    case RadioConstants.Mode:
                        SetMode(item);
                        break;
                    case RadioConstants.Freq:
                        SetFreq(item);
                        break;
                    case RadioConstants.ATUButton:
                        SetAtuButton(item);
                        break;
                    case RadioConstants.VerboseError:
                        SetVerboseError(item);
                        break;
                    case RadioConstants.AG:
                        SetAG(item);
                        break;
                    case RadioConstants.AGF:
                        SetAGF(item);
                        break;
                    default:
                        NotImplemented(item);
                        break;
                }

                if (item.Status == RadioConstants.Ok)
                {
                    cmd.Success++;
                }
                else
                {
                    cmd.Failed++;
                }
            }
        }

        public virtual void SetAGF(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }

        public virtual void GetAGF(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }

        virtual public void SetAG(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }


        private void NotImplemented(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }
        

        public virtual void SetVerboseError(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }

        virtual public void SetAtuButton(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }
        public virtual void GetMode(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }
        public virtual void GetFreq(RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }
        public virtual void SetMode(Common.RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }
        public virtual void SetFreq(Common.RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }
        public virtual void SetVerbose(Common.RadioProperty item)
        {
            item.Status = RadioConstants.NotSupported;
        }        
    }
}
