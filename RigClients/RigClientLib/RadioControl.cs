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
using System.Net.Http;
using System.Net.Http.Headers;
using Wa1gon.Models;
using Wa1gon.Models.Common;

namespace Wa1gon.RigClientLib
{
    public class RadioControl
    {
        static public ServerInfo GetCommPortList(Connection server)
        {
            HttpClient client = new HttpClient();
            
            ServerInfo info;
            string baseUrl;

            try
            {
                baseUrl = server.BuildUriControllerOnly(RadioConstants.InfoController);
                HttpResponseMessage response = client.GetAsync(baseUrl).Result;

                var res = response.Content.ReadAsAsync<ServerInfo>().Result;
                info = res as ServerInfo;
                return info;
            } catch (Exception e)
            {
                var ex = StaticUtils.GetInnerMostException(e);
                throw ex;
            }
        }
        static public List<RadioComConnConfig> GetConnectionList(Connection server)
        {
            HttpClient client = new HttpClient();

            List<RadioComConnConfig> info;


            try
            {
                string baseUrl = server.BuildUriControllerOnly(RadioConstants.ConnectioinController);
                HttpResponseMessage response = client.GetAsync(baseUrl).Result;

                var res = response.Content.ReadAsAsync<List<RadioComConnConfig>>().Result;
                info = res as List<RadioComConnConfig>;
                return info;
            }
            catch (Exception e)
            {
                var ex = StaticUtils.GetInnerMostException(e);
                throw ex;
            }
        }
        static public RadioComConnConfig GetDefaultConnection(Connection serv)
        {
            List<RadioComConnConfig> conn = GetConnectionList(serv);
            RadioComConnConfig conf = conn.Find(c => c.Default == true);
            if (conf == null && conn != null && conn.Count > 0)
            {
                conf = conn[0];
            }
            if (conf == null)
            {
                conf = new RadioComConnConfig();
            }
            return conf;
        }
        static public bool SendCommConf(RadioComConnConfig config, Connection server)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
                 new MediaTypeWithQualityHeaderValue("application/json"));

            string baseUrl = server.BuildUriControllerOnly(RadioConstants.ConnectioinController);
            var response = client.PostAsJsonAsync(baseUrl, config).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static public RadioPropComandList SetRadioProperty(RadioPropComandList cmd, Connection serv)
        {
            string baseUrl = serv.BuildUriControllerOnly(RadioConstants.RadioController);
            var client = new HttpClient();

            HttpResponseMessage response = client.PostAsJsonAsync(baseUrl, cmd).Result;
            var results = response.Content.ReadAsAsync<RadioPropComandList>().Result;
            return results;
        }
        static public RadioPropComandList GetRadioProperty(RadioPropComandList cmd, Connection serv)
        {
            string baseUrl = serv.BuildUriControllerOnly(RadioConstants.RadioController);
            var client = new HttpClient();

            HttpResponseMessage response = client.PutAsJsonAsync(baseUrl, cmd).Result;
            var results = response.Content.ReadAsAsync<RadioPropComandList>().Result;
            return results;
        }
    }
}
