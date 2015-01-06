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
        public List<CommPortConfig> Get()
        {
            var state = ServerState.Create();
            var conList = new List<CommPortConfig>();
            foreach (var act in state.ActiveRadios)
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
        public HttpResponseMessage Post([FromBody]CommPortConfig commConf)
        {
            var resp = new HttpResponseMessage();


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
