using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Wa1gon.Models;
using Wa1gon.Models.Common;
using Wa1gon.ServerInfrastructure;

namespace Wa1gon.RigControl.Controllers
{
    [RoutePrefix("api/v1")]
    public class RadioController : ApiController 
    {
        // GET api/values 
        [Route("api/vi")]
        public List<RadioComConnConfig> Get()
        {
            var state = ServerState.Create();
            var conList = new List<RadioComConnConfig>();
            foreach (var act in state.ActiveRadios)
            {
                conList.Add(act.CommPort);
            }
            return conList;
        }

        // GET api/values/5
        [Route("Radio/{conn}")]
        public RadioComConnConfig Get(string conn)
        {
            RadioComConnConfig rigReading;
            rigReading = GetReading(conn);
            return rigReading;
        }
        [Route("Radio/{conn}")]
        public RadioPropComandList Post(string conn,[FromBody] RadioPropComandList cmd)
        {

            var state = ServerState.Create();
            var ar = state.ActiveRadios.Find(a => a.ConnectionName.ToLower() == conn.ToLower());
            
            ar.RadioControl.SetSettings(cmd);
            return cmd;

        }
        /// <summary> Read Radio properties and put to the client
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        [Route("Radio/{conn}")]
        public RadioPropComandList Put(string conn, [FromBody] RadioPropComandList cmd)
        {

            var state = ServerState.Create();
            var ar = state.ActiveRadios.Find(a => a.ConnectionName.ToLower() == conn.ToLower());

            ar.RadioControl.GetSettings(cmd);
            return cmd;

        }
        [Route("Radio/{conn}/{cmd}")]
        public RadioPropComandList Get(string conn, string cmd)
        {
            Console.WriteLine("id: {0} cmd: {1}", conn, cmd);
            RadioPropComandList radioProps;
            radioProps = new RadioPropComandList();
            var prop = new RadioProperty();
            radioProps.Properties.Add(prop);
            prop.PropertyName = RadioConstants.Mode;
            prop.PropertyValue = RadioConstants.USB;

            prop = new RadioProperty();
            prop.PropertyName = RadioConstants.Mode;
            prop.PropertyValue = "14.076";
            radioProps.Properties.Add(prop);

            switch (cmd)
            {
                case "RM":
                    //var major = ReadMajor(connection);
                    return radioProps;

                default:
                    break;
            }

            //rigReading = GetReading(id);
            return radioProps;
        }

        private RadioComConnConfig GetReading(string id)
        {
            return null;
        }

        [Route("Radio")]
        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        [Route("Radio")]
        public void Delete(int id)
        {
        } 
    }
}
