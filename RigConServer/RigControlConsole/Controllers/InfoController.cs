using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Wa1gon.Models;
using System.IO.Ports;
using Wa1gon.ServerInfrastructure;
namespace Wa1gon.RigControl.Controllers
{
    public class InfoController : ApiController 
    {

        // GET api/values 
        public ServerInfo Get()
        {
            var info = ServerState.Create();
            return info.ServerInfo;
        }      
    }
}
