using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Models;
namespace RigControlConsole
{
    public class AllRadiosController : ApiController 
    {


        // GET api/values 
        public IEnumerable<string> Get()
        {
            return new string[] { 
                "ICOM746", 
                "ICOM746Pro" ,
                "ICOM706",
                "PowerSDR",
                "ICOM7700",
            };
        }
        
    }
}
