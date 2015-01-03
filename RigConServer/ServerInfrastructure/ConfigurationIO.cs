using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.CommonUtils;

namespace Wa1gon.ServerInfrastructure
{
    public class ConfigurationIO

    {
        static public  void Save(string fileName)
        {
            var state = ServerState.Get();
            JsonUtils.Save(fileName, state);
        }
        static public ServerState Restore(string fileName)
        {
            ServerState state = JsonUtils.Restore<ServerState>(fileName);
            return state;
        }
    }
}
