using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Wa1gon.Models.Common;
using Wa1gon.RigClientLib;
using Wa1gon.Models;
using System.Collections.Generic;

namespace IntergrationTest
{
    [TestClass]
    public class GettingTest
    {
        static private Server server;
        [ClassInitialize()]
        static public void TestSetup(TestContext context)
        {

            server = new Server();
            server.HostName = "localhost";
            server.Port = "7301";
            server.DisplayName = "Flex";

            string baseUrl = server.BuildUri(RadioConstants.ConnectioinController);
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<List<RadioComConnConfig>>().Result;

            bool hasFlex = results.Exists(n => n.ConnectionName == "Flex");
            if (hasFlex == false)
            {
                throw new Exception("Flex isn't defined");
            }
        }
        [TestMethod]
        public void GetFlexModeTest()
        {
        }
    }
}
