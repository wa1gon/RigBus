using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace RigControlConsole
{
   public  class FooController : ApiController
    {
        public string [] Get()
        {
            string [] info = new string [] {"foo", "bar"};
            return info;
        }  
  
        public string get(string id)
        {
            return "foo " + id;
        }
        public string get(string id,string cmd)
        {
            return "foo " + id + " " +  cmd;
        }
    }
}
