using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.Models;

namespace RigControlConsole
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
            var info = ServerInfo.Get();

            info.SupportedRadios.Add("PowerSDR");
            info.SupportedRadios.Add("Dummy");
            info.SupportedRadios.Add("ICom746");

            string[] ports = SerialPort.GetPortNames();
            info.CommPorts = ports.ToList();
        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    } 
}
