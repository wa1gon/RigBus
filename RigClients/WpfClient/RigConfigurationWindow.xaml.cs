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
using Wa1gon.Models;
using Wa1gon.RigClientLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wa1gon.RigClientLib.Utils;

namespace Wa1gon.WpfClient
{
    /// <summary>
    /// Interaction logic for Servers.xaml
    /// </summary>
    public partial class RigConfigurationWindow : Window
    {
        public Server Serv { get; set; }
        public Configuration Conf { get; set; }

        public RigConfigurationWindow()
        {
            InitializeComponent();
            Conf = Configuration.Create();
            ServList.ItemsSource = Conf.Servers;

        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            RigConfig conf = new RigConfig();

            conf.RigName = RigNameTb.Text;
            conf.RigType = RigTypeTb.Text;
            conf.Port = ComPortTb.Text;
            conf.Bps = BpsTb.Text.ParseInt();
            conf.StopBits = StopBitsCb.Text.ParseInt();
            conf.DataBits = DataBitsCb.Text.ParseInt();
            conf.Parity = ParityCb.Text;
            conf.Rts = (bool)RtsCb.IsChecked;
            conf.Dtr = (bool)DtrCb.IsChecked;

        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
