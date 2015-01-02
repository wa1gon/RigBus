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
    public partial class RigServersWindow : Window
    {
        public Server Serv { get; set; }
        public Configuration Conf { get; set; }

        public Server SelectedServer { get; set; }

        public RigServersWindow()
        {
            InitializeComponent();
            Conf = Configuration.Create();
            ServList.ItemsSource = Conf.Servers;
            GetSelectedServer();
        }

        private void GetSelectedServer()
        {
            if (Conf.Servers.Count == 0)
            {
                SelectedServer = new Server();
                return;
            }
            SelectedServer = Conf.Servers.Where(s => s.DefaultServer == true).SingleOrDefault();
            if (SelectedServer == null)
            {
                SelectedServer = Conf.GetDefaultServer();
            }
            SetDetailView();
        }


        private void SaveClick(object sender, RoutedEventArgs e)
        {
            Conf = Configuration.Create();


            if (IsDisplayViewValid() == false)  return;
            string host = ServerTb.Text.ToLower();
            string port = PortTb.Text.ToLower();
            string displayName = DisplayNameTb.Text.ToLower();

            if (DefaultServerTb.IsChecked == true)
            {
                Conf.ClearDefaultFromServerList();
            }

            Server serv = Conf.Servers.Where(s => s.DisplayName.ToLower() == displayName).SingleOrDefault();

            if (serv == null)
            {
                AddNewServerToServerList(host, port, displayName);
            }
            else
            {
                ReplaceServerInServerList(host, port, displayName,  serv);
            }

            ServerTb.Text = string.Empty;
            PortTb.Text = string.Empty;
            DisplayNameTb.Text = string.Empty;
            DefaultServerTb.IsChecked = false;
            ServList.ItemsSource = null;
            ServList.ItemsSource = Conf.Servers;

            Conf.Save();

        }


        private void ReplaceServerInServerList(string host, string port, string displayName, Server serv)
        {
            Conf.Servers.Remove(serv);
            serv.DisplayName = displayName;
            serv.Port = port;
            serv.HostName = host;
            serv.HostName = host;
            if (DefaultServerTb.IsChecked != null)
            {
                serv.DefaultServer = (bool)DefaultServerTb.IsChecked;
            }
            else
            {
                serv.DefaultServer = (bool)DefaultServerTb.IsChecked;
            }
            Conf.Servers.Add(serv);
        }

        private void AddNewServerToServerList(string host, string port, string displayName)
        {
            Server serv;
            serv = new Server();
            serv.DisplayName = displayName;
            serv.Port = port;
            serv.HostName = host;
            if (DefaultServerTb.IsChecked != null)
            {
                serv.DefaultServer = (bool)DefaultServerTb.IsChecked;
            }
            else
            {
                serv.DefaultServer = (bool)DefaultServerTb.IsChecked;
            }
            Conf.Servers.Add(serv);
            return ;
        }

        private bool IsDisplayViewValid()
        {
            if (string.IsNullOrWhiteSpace(DisplayNameTb.Text.ToLower()))
            {
                MessageBox.Show("Display Name can not be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(PortTb.Text.ToLower()))
            {
                MessageBox.Show("Port can not be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(ServerTb.Text.ToLower()))
            {
                MessageBox.Show("Host name can not be empty");
                return false;
            }
            return true;
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Server serv = (Server)ServList.SelectedItem;
            Conf.Servers.Remove(serv);
            Conf.Save();
        }

        private void ServList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedServer = ServList.SelectedItem as Server;
            SetDetailView();
        }

        private void SetDetailView()
        {
            ServerTb.Text = SelectedServer.HostName;
            PortTb.Text = SelectedServer.Port;
            DisplayNameTb.Text = SelectedServer.DisplayName;
            DefaultServerTb.IsChecked = SelectedServer.DefaultServer;
        }

        private void NewClick(object sender, RoutedEventArgs e)
        {
            SelectedServer = new Server();
            SetDetailView();
        }
    }
}
