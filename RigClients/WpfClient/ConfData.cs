using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    public class ConfigData
    {
        public ObservableCollection<Server> Servers { get; set; }
        public ConfigData()
        {
            Servers = new ObservableCollection<Server>();
        }
    }
}
