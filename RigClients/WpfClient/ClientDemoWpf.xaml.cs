
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
using System.Windows;
using Wa1gon.Models;
using Wa1gon.RigClientLib;

namespace Wa1gon.WpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ClientDemoWpf : Window
    {
        public ClientDemoWpf()
        {
            InitializeComponent();
        }

        private void Server_Click(object sender, RoutedEventArgs e)
        {
            var servWin = new RigServersWindow();
            servWin.ShowDialog();
        }

        private void Radio_Click(object sender, RoutedEventArgs e)
        {
            var radioWin= new RigConfigurationWindow();
            radioWin.ShowDialog();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Configuration conf = Configuration.Create();
            var defaultServ = conf.GetDefaultServer();
            DefaultServer.Text = defaultServ.DisplayName;
            CommPortConfig defaultCom = RigControl.GetDefaultConnection(defaultServ);
            DefaultRadio.Text = defaultCom.ConnectionName;
        }
    }
}
