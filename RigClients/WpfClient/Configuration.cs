#region -- Copyright
/*
   Copyright {2014} {Darryl Wagoner DE WA1GON}

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;
using Wa1gon.RigClientLib;

namespace Wa1gon.WpfClient
{
    public class Configuration
    {
        static private Configuration conf=null;
        static private object lockobj = new object();
        static public Configuration Create()
        {
            lock (lockobj)
            {
                if (conf == null)
                {
                    conf = new Configuration();
                    conf.configData = new ConfigData();
                    string confPath = conf.GetConfigPath();
                    if (File.Exists(confPath))
                    {
                        conf.Restore();
                    }
                }
                return conf;
            }           
        }
        public ObservableCollection<Server> Servers { 
            get
            {
                return configData.Servers;
            }
            set
            {
                configData.Servers = value;
            }
        }
        private ConfigData configData;

        private Configuration()
        {

        }

        public void Save()
        {

            string confPath = GetConfigPath();
            JsonUtils.Save(confPath,this);
        }

        public void Restore()
        {
            string confPath = GetConfigPath();
            if (File.Exists(confPath))
            {
                configData = JsonUtils.Restore<ConfigData>(confPath);   
            }
            else
            {
                configData = new ConfigData();
            }
            
        }
        private string GetAppPath()
        {
            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (Directory.Exists(local) == false)
            {
                Directory.CreateDirectory(local);
            }
            return local;
        }
        private string GetConfigPath()
        {
            string local = GetAppPath();
            string confPath = local + "/" + "WpfDemoRigControl";
            if (Directory.Exists(confPath) == false)
            {
                Directory.CreateDirectory(confPath);
            }
            confPath = confPath + "/" + "RigControlConf.json";
            return confPath;
        }
    }
}
