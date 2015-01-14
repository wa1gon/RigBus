using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wa1gon.Models;
using Wa1gon.ServerInfrastructure;

namespace Wa1gon.RigControl.Controllers
{

    [RoutePrefix("api/v1")]
    public class V1ConnectionController : ApiController 
    {
        [Route("Connection")]
        public List<RadioComConnConfig> Get()
        {
            var state = ServerState.Create();
            var conList = new List<RadioComConnConfig>();
            foreach( var act in state.ActiveRadios)
            {
                conList.Add(act.CommPort);
            }
            return conList;
        }

        // GET api/values/5
        [Route("Connection")]
        public RadioComConnConfig Get(string id)
        {
            RadioComConnConfig rigReading;
            rigReading = GetReading(id);
            return rigReading;
        }
        private RadioComConnConfig GetReading(string id)
        {
            return null;
        }

        // POST api/values 
        [Route("Connection")]
        public HttpResponseMessage Post([FromBody]RadioComConnConfig value)
        {
            var resp = new HttpResponseMessage();
            if (string.IsNullOrWhiteSpace(value.RadioType) || string.IsNullOrWhiteSpace(value.Port))
            {
                resp.StatusCode = HttpStatusCode.NotFound;
                resp.ReasonPhrase = "Comm port or Rig type is emtpy or null";
                return resp;
            }
            if (value.Bps == null)
            {
                resp.StatusCode = HttpStatusCode.RequestedRangeNotSatisfiable;
                resp.ReasonPhrase = "Bps(baud rate) Can not be null.";
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

            try
            {
                var port = new SerialPort();

                port.PortName = value.Port;
                port.BaudRate = (int)value.Bps;
                port.Parity = GetParity(value.Parity);
                port.DataBits = (int)value.DataBits;
                port.StopBits = GetStopBits(value.StopBits);
                
            } catch (Exception e)
            {
                resp.ReasonPhrase = e.Message;
            }
            state.ActiveRadios.Add(activeRadio);
            ConfigurationIO.Save(state.ConfigFilePath);
         
            resp.ReasonPhrase = "Comm port open!";

            return resp;
        }

        private StopBits GetStopBits(string stopbits)
        {
            switch(stopbits)
            {
                case "none":
                    return StopBits.None;
                case "1":
                    return StopBits.One;
                case "2":
                    return StopBits.Two;
                case "1.5":
                    return StopBits.Two;
            }
            return StopBits.None;

        }

        private Parity GetParity(string p)
        {
            switch (p.ToLower())
            {
                case "even":
                    return Parity.Even;
                case "odd":
                    return Parity.Odd;
                case "space":
                    return Parity.Space;
                case "mark":
                    return Parity.Mark;
                default:
                    return Parity.None;
            }
            //throw new NotImplementedException();
        }

        // PUT api/values/5 
        [Route("Connection")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        [Route("Connection")]
        public void Delete(int id)
        {
        } 
      
    }
}
