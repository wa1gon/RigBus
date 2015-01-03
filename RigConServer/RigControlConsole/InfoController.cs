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
            var info = new ServerInfo();
            info.SupportedRadios = new List<string>();

            info.SupportedRadios.Add("PowerSDR");
            info.SupportedRadios.Add("Dummy");
            info.SupportedRadios.Add("ICom746");

            string[] ports = SerialPort.GetPortNames();
            info.CommPorts = new List<string>(ports);
            return info;
        }

        
    }
}
