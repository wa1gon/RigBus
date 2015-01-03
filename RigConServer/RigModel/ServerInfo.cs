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

namespace Wa1gon.Models
{
    public class ServerInfo 
    {
        static private ServerInfo instance;
        static private object lockobject=new object();
        public List<string> SupportedRadios { get; set; }
        public List<string> CommPorts { get; set; }
        private ServerInfo()
        {
            CommPorts = new List<string>();
            SupportedRadios = new List<string>();        
        }
        public static ServerInfo Get()
        {

            lock (lockobject)
            {
                if (instance == null)
                {
                    instance = new ServerInfo();
                }
            }
            return instance;
        }
    }
}
