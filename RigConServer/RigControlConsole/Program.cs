using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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



            Console.Write("Listening on port: {0}",  port);
            StartOptions options = new StartOptions();
            options.Urls.Add(string.Format("http://localhost:{0}",port));
            options.Urls.Add(string.Format("http://127.0.0.1:{0}",port));
            options.Urls.Add(string.Format("http://{0}:{1}", Environment.MachineName,port));

            // Start OWIN host 
            using (WebApp.Start<Startup>(options)) 
            { 
                // Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();

                //string request = baseAddress + "api/Radio";
                //Console.WriteLine("Request address: " + request);
                //var response = client.GetAsync(request).Result;

                //Console.WriteLine(response);
                //Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.ReadLine(); 
            } 

            Console.ReadLine(); 
        } 
    } 
}
