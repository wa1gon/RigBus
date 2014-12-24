using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Models;
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
            DummyRig reading = (DummyRig)RadioFactory.Get("dummy");
            reading.RigConf.RigName = "Dummy";
            reading.RigConf.RigType = "FlexRadio";
            reading.RigConf.Bps = 9600;
            reading.RigConf.Parity = "None";
            reading.RigConf.Frequency = 14.290;
            reading.RigConf.Mode = "USB";
            return reading.RigConf;
        }
        [Route("api/commports")]
        public IEnumerable<string> GetCategoryId(int categoryId) 
        {
            string[] ports = { "COM1", "COM16" };
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
