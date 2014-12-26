using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Models;

namespace ClientDemo
{
    class Program
    {
        private HttpClient client;
        private string baseUrl;
        private RigConfig config;
        static void Main(string[] args)
        {
            var app = new Program();
            app.getSerial();
            string baseUrl = "http://localhost:9000/api/Radio/foo";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<RigConfig>().Result as RigConfig;

            Console.WriteLine("RigName: " + results.RigName);
            Console.WriteLine("RigType: " + results.RigType);
            Console.WriteLine("Parity: " + results.Parity);
            Console.WriteLine("Stop Bits: " + results.StopBits);
            Console.WriteLine("Bps: " + results.Bps);

            Console.ReadKey();
        }
        public Program()
        {
            client = new HttpClient();
            
        }

        private void getSerial()
        {

            baseUrl = "http://localhost:9000/api/Radio/foo";
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<RigConfig>().Result as RigConfig;

            Console.WriteLine("RigName: " + results.RigName);
            Console.WriteLine("RigType: " + results.RigType);
            Console.WriteLine("Parity: " + results.Parity);
            Console.WriteLine("Stop Bits: " + results.StopBits);
            Console.WriteLine("Bps: " + results.Bps);

            Console.ReadKey();
        }
    }
}
