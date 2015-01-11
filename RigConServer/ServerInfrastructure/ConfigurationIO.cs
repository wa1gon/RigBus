using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.CommonUtils;
using Wa1gon.Models;

namespace Wa1gon.ServerInfrastructure
{
    public class ConfigurationIO

    {
        static public  void Save(string fileName)
        {
            var state = ServerState.Create();
            JsonUtils.Save(fileName, state);
        }
        static public ServerState Restore(string fileName)
        {
            ServerState state = JsonUtils.Restore<ServerState>(fileName);
            foreach(var radio in state.ActiveRadios)
            {
                radio.RadioControl = RadioFactory.Get(radio.CommPort.RadioType);

            }
            return state;
        }
    }
}
