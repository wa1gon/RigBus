using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Wa1gon.Models;
using System.Collections.Generic;
using System.Net;

namespace IntergrationTest
{
    [TestClass]
    public class SettingsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string baseUrl = "http://localhost:7301/api/Connection";
            var client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(baseUrl).Result;

            var results = response.Content.ReadAsAsync<List<CommPortConfig>>().Result;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
