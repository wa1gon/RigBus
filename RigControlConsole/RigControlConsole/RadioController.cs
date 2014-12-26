using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Models;
using System.IO.Ports;
using System.Net.Http;
using System.Net;
namespace RigControlConsole
{

    public class RadioController : ApiController 
    {
        


        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public RigConfig Get(string id)
        {
            RigConfig rigReading;
            rigReading = GetReading(id);
            return rigReading;
        }

        private RigConfig GetReading(string id)
        {
            DummyMaster reading = (DummyMaster)RadioFactory.Get("dummy");
            reading.Config = new RigConfig();

            reading.Config.RigName = "Dummy";
            reading.Config.RigType = "FlexRadio";
            reading.Config.Bps = 9600;
            reading.Config.Parity = "None";
            return reading.Config;
        }

        public IEnumerable<string> GetCategoryId(int categoryId) 
        {
            string[] ports = SerialPort.GetPortNames();
            return ports;
        }
        // POST api/values 
        public HttpResponseMessage Post([FromBody]RigConfig value)
        {
            var resp = new HttpResponseMessage();
            if (value.RigType == null)
            {
                resp.StatusCode = HttpStatusCode.NotFound;
            }
            else
            {
                resp.StatusCode = HttpStatusCode.NoContent;
                resp.ReasonPhrase = "Comm port open!";

            }
            return resp;
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        } 

        
    }
}
