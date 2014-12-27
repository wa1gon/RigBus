using System;
using System.Collections.Generic;

namespace Models
{
    /// <summary>
    /// Description of ActiveRadios
    /// </summary>
    public sealed class ActiveRadios
    {
        private static ActiveRadios instance = new ActiveRadios();
        public Dictionary<string,RigConfig> ActiveList { get; set; }
        public static ActiveRadios Instance
        {
            get
            {
                return instance;
            }
        }

        private ActiveRadios()
        {
            ActiveList = new Dictionary<string, RigConfig>();
        }
        public void AddRadio(RigConfig rig)
        {
            ActiveList.Add(rig.RigName, rig);
        }
        public void RemoveRadio(string name)
        {
            ActiveList.Remove(name);
        }
        public RigConfig GetActiveByName(string name)
        {
            try
            {
                var config = ActiveList[name];
                return config;
            }
            catch (Exception)
            {
                var errorConfig = new RigConfig();
                errorConfig.Status = "Active Configuration not found!";
                return errorConfig;
            }
        }
    }
}
