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
using System.Windows;
using Wa1gon.Models;
using System.Collections.Generic;
using Wa1gon.RigClientLib;
using Wa1gon.RigClientLib.Utils;

namespace Wa1gon.WpfClient
{
    /// <summary>
    /// Interaction logic for Servers.xaml
    /// </summary>
    public partial class RigConfigurationWindow : Window
    {
        private string ErrorMessage;
        public Server Serv { get; set; }
        public Configuration Conf { get; set; }

        public List<CommPortConfig> ConnList { get; set; }

        public RigConfigurationWindow()
        {
            Conf = Configuration.Create();
            
            InitializeComponent();


            // for debug
            RigTypeCombo.Text="Dummy";
            ComPortCombo.Text="COM5";
            RigTypeCombo.Text="Dummy";
            BpsTb.Text="19200";
            StopBitsCb.Text = "1";
            DataBitsCb.Text = "7";
            ParityCb.Text = "NONE";
            RtsCb.IsChecked = false;
            DtrCb.IsChecked = false;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            CommPortConfig conf = new CommPortConfig();
            conf.ConnectionName = ConectionNameTb.Text;
            conf.RadioType = RigTypeCombo.Text;
            conf.Port = ComPortCombo.Text;
            conf.Bps = BpsTb.Text.ParseInt();
            conf.StopBits = StopBitsCb.Text.ParseInt();
            conf.DataBits = DataBitsCb.Text.ParseInt();
            conf.Parity = ParityCb.Text;
            conf.Rts = (bool)RtsCb.IsChecked;
            conf.Dtr = (bool)DtrCb.IsChecked;

            if (isValid(conf) == false)
            {
                MessageBox.Show(ErrorMessage);
            }
            else
            {

                bool rc = RigControl.SendCommConf(conf, Serv);
            }

        }

        private bool isValid(CommPortConfig conf)
        {
            if (conf.RadioType.Length == 0)
            {
                ErrorMessage = "Rig Type can't be blank!";
                return false;
            }
            if (conf.ConnectionName.Length == 0)
            {
                ErrorMessage = "Connection Name can't be blank!";
                return false;
            }
            if (conf.Port.Length == 0)
            {
                ErrorMessage = "Comm port can't be blank!";
                return false;
            }
            return true;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Serv = Conf.GetDefaultServer();
            if (Serv == null)
            {
                MessageBox.Show("There is no Server defined.");
                Close();
                return;
            }
            Conf = Configuration.Create();
            RigServersCombo.ItemsSource = Conf.Servers;
            RigServersCombo.SelectedItem = Serv;
            try
            {
                var servInfo = RigControl.GetCommPortList(Serv);
                ConnList = RigControl.GetConnectionList(Serv);
                ComPortCombo.ItemsSource = servInfo.AvailCommPorts;
                RigTypeCombo.ItemsSource = servInfo.SupportedRadios;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                Close();
                return;
            }
            DataContext = this;
        }

        private void ConnLV_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CommPortConfig conf = ConnLV.SelectedItem as CommPortConfig;
            if (conf == null) {return;}

            RigTypeCombo.SelectedItem = conf.RadioType;

            // with out this line the comm port doesn't show up.  WPF bug?
            ComPortCombo.Text = "";
            ComPortCombo.Text = conf.Port;
            BpsTb.Text = conf.Bps.ToString();
            StopBitsCb.Text = conf.StopBits.ToString();
            DataBitsCb.Text = conf.DataBits.ToString();
            ParityCb.Text = conf.Parity;
            RtsCb.IsChecked = conf.Rts;
            DtrCb.IsChecked = conf.Dtr;
            ConectionNameTb.Text = conf.ConnectionName;
        }
    }
}
