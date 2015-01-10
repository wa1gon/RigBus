using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wa1gon.Models.Validations;

namespace Wa1gon.Models.Common
{
    /// <summary> Contains the setting for the radio.  
    /// 
    /// </summary>
    public class SettingValue
    {
        public string Setting { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public BaseValidator Validator { get; set; }
    }
}
