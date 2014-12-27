using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO;

namespace WpfClient
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
