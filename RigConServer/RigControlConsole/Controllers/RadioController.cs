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
    public class RadioController : ApiController 
    {
        // GET api/values 
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
        public RadioComConnConfig Get(string connection)
        {
            RadioComConnConfig rigReading;
            rigReading = GetReading(connection);
            return rigReading;
        }
        public RadioPropComandList Post(string connection,[FromBody] RadioPropComandList cmd)
        {

            var state = ServerState.Create();
            var ar = state.ActiveRadios.Find(a => a.ConnectionName.ToLower() == connection.ToLower());
            
            ar.RadioControl.SetSettings(cmd);
            return cmd;

        }
        /// <summary> Read Radio properties and put to the client
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public RadioPropComandList Put(string connection, [FromBody] RadioPropComandList cmd)
        {

            var state = ServerState.Create();
            var ar = state.ActiveRadios.Find(a => a.ConnectionName.ToLower() == connection.ToLower());

            ar.RadioControl.GetSettings(cmd);
            return cmd;

        }
        public RadioPropComandList Get(string connection, string cmd)
        {
            Console.WriteLine("id: {0} cmd: {1}", connection, cmd);
            RadioPropComandList radioProps;
            radioProps = new RadioPropComandList();
            var prop = new RadioProperty();
            radioProps.Settings.Add(prop);
            prop.PropertyName = RadioConstants.Mode;
            prop.PropertyValue = RadioConstants.USB;

            prop = new RadioProperty();
            prop.PropertyName = RadioConstants.Mode;
            prop.PropertyValue = "14.076";
            radioProps.Settings.Add(prop);

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
        private MajorSettings ReadMajor(string connId)
        {
            var state = ServerState.Create();
            return null;
        }
        private RadioComConnConfig GetReading(string id)
        {
            return null;
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
