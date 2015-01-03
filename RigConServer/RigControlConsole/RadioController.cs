using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Wa1gon.Models;
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
        public CommPortConfig Get(string id)
        {
            CommPortConfig rigReading;
            rigReading = GetReading(id);
            return rigReading;
        }

        private CommPortConfig GetReading(string id)
        {
            DummyMaster reading = (DummyMaster)RadioFactory.Get("dummy");
            reading.Config = new CommPortConfig();

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
        public HttpResponseMessage Post([FromBody]CommPortConfig value)
        {
            var resp = new HttpResponseMessage();
            if (string.IsNullOrWhiteSpace(value.RigType) || string.IsNullOrWhiteSpace(value.Port))
            {
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.ReasonPhrase = "Comm port or Rig type is emtpy or null";
                return resp;
            }
            var servInfo = ServerInfo.Get();

            bool hasComm = servInfo.CommPorts.Contains(value.Port);
            if (hasComm == false)
            {
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.ReasonPhrase = "Comm port not found!";
                return resp;
            }
            bool isRadioSupported = servInfo.SupportedRadios.Contains(value.RigType);
            if (isRadioSupported == false)
            {
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.ReasonPhrase = "Radio not supported!";
                return resp;
            }
            resp.StatusCode = HttpStatusCode.NoContent;
            resp.ReasonPhrase = "Comm port open!";

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
