using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wa1gon.Models;
using Wa1gon.ServerInfrastructure;

namespace Wa1gon.RigControl.Controllers
{

    public class ConnectionController : ApiController 
    {
       
        // GET api/values 
        public List<CommPortConfig> Get()
        {
            var state = ServerState.Create();
            var conList = new List<CommPortConfig>();
            foreach( var act in state.ActiveRadios)
            {
                conList.Add(act.CommPort);
            }
            return conList;
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
            return null;
        }

        // POST api/values 
        public HttpResponseMessage Post([FromBody]CommPortConfig value)
        {
            var resp = new HttpResponseMessage();
            if (string.IsNullOrWhiteSpace(value.RadioType) || string.IsNullOrWhiteSpace(value.Port))
            {
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.ReasonPhrase = "Comm port or Rig type is emtpy or null";
                return resp;
            }
            var servState = ServerState.Create();

            bool hasComm = servState.ServerInfo.AvailCommPorts.Contains(value.Port);
            if (hasComm == false)
            {
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.ReasonPhrase = "Comm port not found!";
                return resp;
            }
            bool isRadioSupported = servState.ServerInfo.SupportedRadios.Contains(value.RadioType);
            if (isRadioSupported == false)
            {
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.ReasonPhrase = "Radio not supported!";
                return resp;
            }
            resp.StatusCode = HttpStatusCode.NoContent;
            var state = ServerState.Create();

            var activeRadio = state.ActiveRadios.
                Find(a => a.ConnectionName == value.ConnectionName);
            if (activeRadio == null)
            {
                activeRadio = new ActiveRadio();
            }
            else
            {
                state.ActiveRadios.Remove(activeRadio);
            }

            activeRadio.ConnectionName = value.ConnectionName;
            activeRadio.CommPort = value;
            activeRadio.RadioControl = RadioFactory.Get(value.RadioType);
            state.ActiveRadios.Add(activeRadio);
            ConfigurationIO.Save(state.ConfigFilePath);
         
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
