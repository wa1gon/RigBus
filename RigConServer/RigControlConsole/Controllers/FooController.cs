using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Wa1gon.RigControl.Controllers
{
   public  class FooController : ApiController
    {
        public string [] Get()
        {
            string [] info = new string [] {"foo", "bar"};
            return info;
        }  
  
        public string [] get(string id)
        {
            return new string [] {"foo", id};
        }
        public List<string> get(string id,string cmd)
        {
            var ar = new List<string> ();
            ar.Add("f00");
            ar.Add(id);
            ar.Add(cmd);
            ar.Add("horse");

            return ar;
        }
    }
}
