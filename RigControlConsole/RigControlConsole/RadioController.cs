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
        static RigModel reading = new RigModel();


        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public RigModel Get(string id)
        {
            RigModel rigReading=null;
            rigReading = GetReading(id);
            return rigReading;
        }

        private RigModel GetReading(string id)
        {
            var reading = new RigModel();
            reading.RigName = "Dummy";
            reading.RigType = "FlexRadio";
            reading.Bps = 9600;
            reading.Parity = "None";
            reading.Frequency = 14.290;
            reading.Mode = "USB";
            return reading;
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
