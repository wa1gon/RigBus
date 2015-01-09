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

namespace Wa1gon.Models
{
    /// <summary> Description of ActiveRadios
    /// </summary>
    public sealed class ActiveRadios
    {
        private static ActiveRadios instance = new ActiveRadios();
        public Dictionary<string,RadioComConnConfig> ActiveCommPortList { get; set; }
        public static ActiveRadios Create()
        {
                return instance;
        }

        private ActiveRadios()
        {
            ActiveCommPortList = new Dictionary<string, RadioComConnConfig>();
        }
        public void AddRadio(RadioComConnConfig rig)
        {
            ActiveCommPortList.Add(rig.ConnectionName, rig);
        }
        public void RemoveRadio(string name)
        {
            ActiveCommPortList.Remove(name);
        }
        public RadioComConnConfig GetActiveByName(string name)
        {
            try
            {
                var config = ActiveCommPortList[name];
                return config;
            }
            catch (Exception)
            {
                var errorConfig = new RadioComConnConfig();
                errorConfig.Status = "Active Configuration not found!";
                return errorConfig;
            }
        }
    }
}
