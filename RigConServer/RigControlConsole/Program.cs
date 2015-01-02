using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace RigControlConsole
{
    class Program
    {
        static void Main(string [] args) 
        {

            string port = "7300";

            if (args.Length == 1)
            {
                port = args[0];
            }

            Console.WriteLine("Listening on port: {0}",  port);
            StartOptions options = new StartOptions();
            options.Urls.Add(string.Format("http://localhost:{0}",port));

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

            Console.ReadLine(); 

        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    } 
}
