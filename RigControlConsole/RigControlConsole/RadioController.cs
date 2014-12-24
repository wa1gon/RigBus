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
        static IRigModel reading = RadioFactory.Get("dummy");


        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public IRigModel Get(string id)
        {
            IRigModel rigReading=null;
            rigReading = GetReading(id);
            return rigReading;
        }

        private IRigModel GetReading(string id)
        {

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
