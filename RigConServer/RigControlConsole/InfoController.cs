using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Wa1gon.Models;
using System.IO.Ports;
namespace RigControlConsole
{
    public class InfoController : ApiController 
    {

        // GET api/values 
        public ServerInfo Get()
        {
            var info = ServerInfo.Get();
            return info;
        }      
    }
}
