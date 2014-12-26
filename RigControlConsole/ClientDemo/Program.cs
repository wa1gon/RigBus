using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Models;
using System.Net.Http.Headers;

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

           app.SetRig();

            Console.ReadKey();
        }

        private void SetRig()
        {
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            config.Command = "Open";
            config.RigName = "myDummy";
            config.RigType = "Dummy";
            var response = client.PostAsJsonAsync("http://localhost:9000/api/Radio", config).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Dummy connection open"); 
            }
            else
            {
                Console.WriteLine("Error Code" + response.StatusCode + 
                    " : Message - " + response.ReasonPhrase);
            }

        }
        public Program()
        {
            client = new HttpClient();          
        }

        private void getSerial()
        {

            baseUrl = "http://localhost:9000/api/Radio/foo";
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            config = response.Content.ReadAsAsync<RigConfig>().Result as RigConfig;

            Console.WriteLine("RigName: " + config.RigName);
            Console.WriteLine("RigType: " + config.RigType);
            Console.WriteLine("Parity: " + config.Parity);
            Console.WriteLine("Stop Bits: " + config.StopBits);
            Console.WriteLine("Bps: " + config.Bps);
        }
    }
}
