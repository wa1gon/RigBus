using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Wa1gon.Models.Common;
using Wa1gon.RigClientLib;
using Wa1gon.Models;
using System.Collections.Generic;
using System.Net;

namespace IntergrationTest
{
    [TestClass]
    public class GettingTest
    {
        static private Connection server;
        [ClassInitialize()]
        static public void TestSetup(TestContext context)
        {

            server = new Connection();
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
        public void GetFlexFreqTest()
        {
            string baseUrl = server.BuildUri(RadioConstants.RadioController);
            var client = new HttpClient();

            var cmdReq = new RadioPropComandList();
            var rigProp = new RadioProperty();
            rigProp.PropertyName = RadioConstants.Freq;
            rigProp.PropertyValue = "";
            rigProp.Vfo = RadioConstants.VfoA;
            cmdReq.Settings.Add(rigProp);

            var retCmd = RadioControl.GetRadioProperty(cmdReq, server);

            Assert.AreEqual(1, retCmd.Success);
        }
        [TestMethod]
        public void GetFlexModeTest()
        {
            string baseUrl = server.BuildUri(RadioConstants.RadioController);
            var client = new HttpClient();

            var cmdReq = new RadioPropComandList();
            var rigProp = new RadioProperty();
            rigProp.PropertyName = RadioConstants.Mode;
            rigProp.PropertyValue = "";
            rigProp.Vfo = RadioConstants.VfoA;
            cmdReq.Settings.Add(rigProp);

            var retCmd = RadioControl.GetRadioProperty(cmdReq, server);
            Assert.AreEqual(1, retCmd.Success);
        }
    }
}
