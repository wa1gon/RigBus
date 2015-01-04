using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.Models;


namespace Wa1gon.ServerInfrastructure
{
    public class ActiveRadio
    {
        public string ConnectionName { get; set; }
        public CommPortConfig CommPort { get; set; }
        public RadioControlBase RadioControl { get; set; }
    }
}
