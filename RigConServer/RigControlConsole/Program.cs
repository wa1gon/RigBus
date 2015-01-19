using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.Models;
using Wa1gon.Models.Common;
using Wa1gon.ServerInfrastructure;

namespace Wa1gon.RigControl
{
    /// <summary>  This is the main for the self hosted Rigcontroller. 
    /// The production version this should be a service. 
    /// 
    /// </summary>
    class Program
    {
        static void Main(string [] args) 
        {
            Program mainServer = new Program();
            mainServer.InitServerState();
            string port = "7300";

            if (args.Length == 1)
            {
                port = args[0];
            }

            mainServer.StartServer(port);
        }

        private void StartServer(string port)
        {
            Console.WriteLine("Listening on port: {0}", port);
            StartOptions options = new StartOptions();
            options.Urls.Add(string.Format("http://localhost:{0}", port));
            Console.WriteLine("Opening up access to local clients only.");
            // Need to be admin to listen on remote address
            if (IsAdministrator() == true)
            {
                Console.WriteLine("Opening up access to remote clients.");
                options.Urls.Add(string.Format("http://127.0.0.1:{0}", port));
                options.Urls.Add(string.Format("http://{0}:{1}", Environment.MachineName, port));
            }

            // Start OWIN host 
            using (WebApp.Start<Startup>(options))
            {
                Console.ReadLine();
            }
        }

        private void InitServerState()
        {
            string confFile = "C:/Stargate/RigGateServer.json";
            ServerState info;
            if (File.Exists(confFile))
            {
                info = ConfigurationIO.Restore(confFile);
                ServerState.Create(info);

            }
            else
            {
                info = ServerState.Create();
                ConfigurationIO.Save(confFile);
            }

            // this should be done every time as the server and/or comm ports support 
            // has changed.
            EnumSupportedRadios(info);
            InitAvailCommPort(info);
            info.ConfigFilePath = confFile;
            info.ReLinkConnections();

        }



        private static void EnumSupportedRadios(ServerState info)
        {
            AddRadioIfNew(RadioConstants.PowerSDR, info.ServerInfo);
            AddRadioIfNew(RadioConstants.DummyRadio, info.ServerInfo);
            AddRadioIfNew(RadioConstants.Icom746, info.ServerInfo); 
        }
        private static void AddRadioIfNew(string radioType, ServerInfo sInfo)
        {
            if (sInfo.SupportedRadios.Contains(RadioConstants.PowerSDR) == false)
            {
                sInfo.SupportedRadios.Add(radioType);
            }
        }

        private static void InitAvailCommPort(ServerState info)
        {
            string[] ports = SerialPort.GetPortNames();
            info.ServerInfo.AvailCommPorts = ports.ToList();
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    } 
}
