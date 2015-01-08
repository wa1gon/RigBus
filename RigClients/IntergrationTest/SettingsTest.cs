using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Wa1gon.Models;
using System.Collections.Generic;
using System.Net;

namespace IntergrationTest
{

    /// <summary>  These are not unit test but integration test for the test
    /// to work correct there must be a connection with the name of "Flex" connected to 
    /// a PowerSDR.  The PowerSDR can be running in demo mode, as well as the RigControlServer
    /// 
    /// </summary>


    [TestClass]
    public class SettingsTest
    {

        private RadioComConnConfig connConf;
        [ClassInitialize]
        void TestSetup()
        {
           

        }
        [ClassCleanup]
        void TestCleanup()
        {

        }
        [TestMethod]
        public void GetListOfConnections()
        {
            string baseUrl = "http://localhost:7301/api/Connection";
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<List<RadioComConnConfig>>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);


        }
    }
}
