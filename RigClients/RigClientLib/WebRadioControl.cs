using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.Models;

namespace Wa1gon.RigClientLib
{
    public class WebRadioControl
    {
        public ServerInfo GetServerInfo(Server serv )
        {
            ServerInfo info = new ServerInfo();
            string baseUrl;
            baseUrl = "http:" + serv.HostName + ":" + serv.Port.ToString() + "/api/Radio/foo";
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            config = response.Content.ReadAsAsync<RigConfig>().Result as RigConfig;

           // GetServerInfo 
            return null;
        }

    }
}
