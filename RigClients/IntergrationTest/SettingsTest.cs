using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Wa1gon.Models;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Wa1gon.Models.Common;
using Wa1gon.RigClientLib;

namespace IntergrationTest
{

    /// <summary>  These are not unit test but integration test for the test
    /// to work correct there must be a connection with the name of "Flex" connected to 
    /// a PowerSDR.  The PowerSDR can be running in demo mode, as well as the RigControlServer
    /// </summary>

    [TestClass()]
    public class SettingsTest
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

        #region unused test init
        //[AssemblyInitialize()]
        //public static void AssemblyInit(TestContext context)
        //{
        //    MessageBox.Show("AssemblyInit " + context.TestName);
        //}

        //[ClassInitialize()]
        //public static void ClassInit(TestContext context)
        //{
        //    MessageBox.Show("ClassInit " + context.TestName);
        //}

        //[TestInitialize()]
        //public void Initialize()
        //{
        //    MessageBox.Show("TestMethodInit");
        //}

        //[TestCleanup()]
        //public void Cleanup()
        //{
        //    MessageBox.Show("TestMethodCleanup");
        //}

        //[ClassCleanup()]
        //public static void ClassCleanup()
        //{
        //    MessageBox.Show("ClassCleanup");
        //}

        //[AssemblyCleanup()]
        //public static void AssemblyCleanup()
        //{
        //    MessageBox.Show("AssemblyCleanup");
        //}
        #endregion


        [TestMethod]
        public void GetListOfConnections()
        {
            string baseUrl = "http://localhost:7301/api/Connection";
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<List<RadioComConnConfig>>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void ReadMajorTest()
        {
            string baseUrl = "http://localhost:7301/api/Radio/Flex/RM";
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<MajorSettings>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

       // [TestMethod]
        public void PostSetModeDummyTest()
        {
            string baseUrl = server.BuildUri("Radio");
               // "http://localhost:7301/api/Radio/Dummy/";
            var client = new HttpClient();

            var cmdReq = new RadioPropComandList();
            var setting = new RadioProperty();
            setting.PropertyName = RadioConstants.Mode;
            setting.PropertyValue = RadioConstants.USB;
            cmdReq.Settings.Add(setting);

            var respCmd = RadioControl.SetRadioProperty(cmdReq, server);

            Assert.AreEqual(1, respCmd.Success);
        }

        [TestMethod]
        public void PostSetModeFlexTest()
        {
            string baseUrl = server.BuildUri(RadioConstants.RadioController);

            var client = new HttpClient();

            var cmdReq = new RadioPropComandList();
            var setting = new RadioProperty();
            setting.PropertyName = RadioConstants.Mode;
            setting.PropertyValue = RadioConstants.USB;
            setting.Vfo = RadioConstants.VfoA;
            cmdReq.Settings.Add(setting);
            var respCmd = RadioControl.SetRadioProperty(cmdReq, server);

            Assert.AreEqual(1, respCmd.Success);
            Assert.AreEqual(1, respCmd.Settings.Count);
        }

        [TestMethod]
        public void PostSetFreqFlexTest()
        {
            string baseUrl = server.BuildUri(RadioConstants.RadioController);
            var client = new HttpClient();

            var cmdReq = new RadioPropComandList();
            var rigProp = new RadioProperty();
            rigProp.PropertyName = RadioConstants.Freq;
            rigProp.PropertyValue = "14.120";
            rigProp.Vfo = "a";
            cmdReq.Settings.Add(rigProp);

            var respCmd = RadioControl.SetRadioProperty(cmdReq, server);

            //HttpResponseMessage response = client.PostAsJsonAsync(baseUrl, cmdReq).Result;

            //var results = response.Content.ReadAsAsync<RadioPropComandList>().Result;
            Assert.AreEqual(1, respCmd.Success);

            rigProp.PropertyName = RadioConstants.Freq;
            rigProp.PropertyValue = "7.223";
            rigProp.Vfo = "b";
            cmdReq.Settings.Add(rigProp);
            var cmdResp = RadioControl.SetRadioProperty(cmdReq,server);

            //Assert.AreEqual(1, results.Success);
        }
    }
}
