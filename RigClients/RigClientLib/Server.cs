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

using System.ComponentModel;
using Wa1gon.Models;

namespace Wa1gon.RigClientLib
{
    public class Connection : BindableObject, INotifyPropertyChanged
    {
        public int Handle { get; set; }
        public string DisplayName { 
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
                NotifyPropertyChanged(() => DisplayName);
            }
        }
        public string HostName { get; set; }
        public string Port { get; set; }
        public bool DefaultServer { get; set; }

        public string BuildUri(string controller)
        {
            string rc = null;
            if (IsValid() == false) return rc;

            rc = string.Format("http://{0}:{1}/api/{2}/{3}", 
                HostName, Port,controller,DisplayName);
            return rc;
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(DisplayName)) return false;
            if (string.IsNullOrWhiteSpace(Port)) return false;
            if (string.IsNullOrWhiteSpace(HostName)) return false;

            return true;
        }
        public override string ToString()
        {
            return DisplayName;
        }

        private string displayName;
    }
}
