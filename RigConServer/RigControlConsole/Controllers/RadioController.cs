using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Wa1gon.Models;
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
        public RadioCmd Post(string connection,[FromBody] RadioCmd cmd)
        {

            return cmd;

        }
        public MajorSettings Get(string connection, string cmd)
        {
            Console.WriteLine("id: {0} cmd: {1}", connection, cmd);
            MajorSettings settings;
            settings = new MajorSettings();
            settings.Mode = "USB";
            settings.Freq = "14.076";

            switch (cmd)
            {
                case "RM":
                    //var major = ReadMajor(connection);
                    return settings;

                default:
                    break;
            }

            //rigReading = GetReading(id);
            return settings;
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
