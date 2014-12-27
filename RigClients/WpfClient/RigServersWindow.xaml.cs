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

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for Servers.xaml
    /// </summary>
    public partial class RigServersWindow : Window
    {
        public Server Serv { get; set; }
        public Configuration Conf { get; set; }

        public RigServersWindow()
        {
            InitializeComponent();
            Conf = Configuration.Create();
            ServList.ItemsSource = Conf.Servers;

        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            Conf = Configuration.Create();
            string server = ServerTb.Text.ToLower();
            string port = PortTb.Text.ToLower();
            string displayName = DisplayNameTb.Text.ToLower();

            if (string.IsNullOrWhiteSpace(displayName))
            {
                MessageBox.Show("Display Name can not be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(port))
            {
                MessageBox.Show("Port can not be empty");
                return;
            }
            if (string.IsNullOrWhiteSpace(displayName))
            {
                MessageBox.Show("Host name can not be empty");
                return;
            }

            Server serv = Conf.Servers.Where(s => s.HostName.ToLower() == server).SingleOrDefault();

            if (serv == null)
            {
                serv = new Server();
                serv.DisplayName = displayName;
                serv.Port = port;
                serv.HostName = server;
                Conf.Servers.Add(serv);
            }
            else
            {
                Conf.Servers.Remove(serv);
                serv.DisplayName = displayName;
                serv.Port = port;
                serv.HostName = server;
                Conf.Servers.Add(serv);
            }

            ServerTb.Text = string.Empty;
            PortTb.Text = string.Empty;
            DisplayNameTb.Text = string.Empty;
            ServList.ItemsSource = Conf.Servers;
            Conf.Save();

        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            Server serv = (Server)ServList.SelectedItem;
            Conf.Servers.Remove(serv);
            Conf.Save();
        }
    }
}
