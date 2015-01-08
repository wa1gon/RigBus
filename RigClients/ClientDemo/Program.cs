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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Wa1gon.Models;
using System.Net.Http.Headers;

namespace ClientDemo
{
    class Program
    {
        private HttpClient client;
        private string baseUrl;
        private RadioComConnConfig config;
        static void Main(string[] args)
        {
            var app = new Program();
            app.getSerial();
            string baseUrl = "http://localhost:7301/api/Connection";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<RadioComConnConfig>().Result as RadioComConnConfig;

            Console.WriteLine("RigName: " + results.ConnectionName);
            Console.WriteLine("RigType: " + results.RadioType);
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
            config.ConnectionName = "myDummy";
            config.RadioType = "Dummy";
            config.Port = "COM6";
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

            config.Command = "Open";
            config.ConnectionName = "myDummy";
            config.RadioType = "Dummy";
            config.Port = "COM40";
            response = client.PostAsJsonAsync("http://localhost:9000/api/Radio", config).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Dummy connection open");
            }
            else
            {
                Console.WriteLine("Error Code" + response.StatusCode +
                    " : Message - " + response.ReasonPhrase);
            }

            config.Command = "Open";
            config.ConnectionName = "myDummy";
            config.RadioType = "Dummy";
            config.Port = "COM6";
            response = client.PostAsJsonAsync("http://localhost:9000/api/Radio", config).Result;
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Dummy connection open");
            }
            else
            {
                Console.WriteLine("Error Code" + response.StatusCode +
                    " : Message - " + response.ReasonPhrase);
            }
            config.Command = "Open";
            config.ConnectionName = "myDummy";
            config.RadioType = "foobar";
            config.Port = "COM6";
            response = client.PostAsJsonAsync("http://localhost:9000/api/Radio", config).Result;
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

            config = response.Content.ReadAsAsync<RadioComConnConfig>().Result as RadioComConnConfig;

            Console.WriteLine("RigName: " + config.ConnectionName);
            Console.WriteLine("RigType: " + config.RadioType);
            Console.WriteLine("Parity: " + config.Parity);
            Console.WriteLine("Stop Bits: " + config.StopBits);
            Console.WriteLine("Bps: " + config.Bps);
        }
    }
}
