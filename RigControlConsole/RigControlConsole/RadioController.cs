using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Models;
using System.IO.Ports;
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
        [Route("coms")]
        public IEnumerable<string> GetCategoryId(int categoryId) 
        {
            string[] ports = SerialPort.GetPortNames();
            return ports;
        }
        // POST api/values 
        public void Post([FromBody]string value)
        {
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
