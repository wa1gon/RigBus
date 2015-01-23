using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wa1gon.RigClientLib;
using Wa1gon.Models.Common;
using System.Collections.Generic;
using Wa1gon.Models;

namespace IntergrationTest.FlexTest
{
    [TestClass]
    public class FlexAudioTest
    {
        static private Connection server;
        [ClassInitialize()]
        static public void TestSetup(TestContext context)
        {

            server = new Connection();
            server.HostName = "localhost";
            server.Port = "7301";
            server.DisplayName = "Flex";

            bool hasFlex = RadioControl.IsConnectionValid(server);
            if (hasFlex == false)
            {
                throw new Exception("Flex isn't defined");
            }
        }
        [TestMethod]
        public void AudioGainTest()
        {
            var cmdReq = new RadioPropComandList();
            var rigProp = new RadioProperty();
            rigProp.PropertyName = RadioConstants.AG;
            rigProp.PropertyValue = "25";
            cmdReq.Properties.Add(rigProp);

            var respCmd = RadioControl.SetRadioProperty(cmdReq, server);

            Assert.AreEqual(1, respCmd.Success);


            Assert.AreEqual(0, respCmd.Failed);
            Assert.AreEqual(1, respCmd.Properties.Count);

            cmdReq.Properties.Clear();
            rigProp.PropertyName = RadioConstants.AG;
            cmdReq.Properties.Add(rigProp);
            respCmd = RadioControl.GetRadioProperty(cmdReq, server);

            Assert.AreEqual(1, respCmd.Success);
            Assert.AreEqual(1, respCmd.Properties.Count);
            Assert.AreEqual("25", respCmd.Properties[0].PropertyValue);
        }
        [TestMethod]
        public void AudioGainGetTest()
        {
            var cmdReq = new RadioPropComandList();
            var rigProp = new RadioProperty();
            rigProp.PropertyName = RadioConstants.AG;

            cmdReq.Properties.Clear();
            rigProp.PropertyName = RadioConstants.AG;
            cmdReq.Properties.Add(rigProp);
            var respCmd = RadioControl.GetRadioProperty(cmdReq, server);

            Assert.AreEqual(1, respCmd.Success);
            Assert.AreEqual(1, respCmd.Properties.Count);
            Assert.AreEqual("35", respCmd.Properties[0].PropertyValue);
        }
    }
}
