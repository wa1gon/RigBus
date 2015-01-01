#region -- Copyright
/*
   Copyright 2014 Darryl Wagoner DE WA1GON

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 * test 1
*/
#endregion

using Wa1gon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.RigClientLib;

namespace Wa1gon.WpfClient
{
    public class ConfigData
    {
        public ObservableCollection<Server> Servers { get; set; }
        public ObservableCollection<RigConfig> RigConfs { get; set; }
        public ConfigData()
        {
            Servers = new ObservableCollection<Server>();
            RigConfs = new ObservableCollection<RigConfig>();
        }
    }
}
