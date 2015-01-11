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
    public class RadioProperty
    {
        /// <summary> Property of the radio to be set
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>  Value of radio properties
        /// </summary>
        public string PropertyValue { get; set; }
        /// <summary> Status of request.  Success is "OK".  Otherwise an error message.
        /// </summary>
        public string Status { get; set; }
        public string Vfo { get; set; }
        /// <summary> Radio Id for radios that share a common serial port such as 
        /// Icoms.
        /// </summary>
        public string RadioId { get; set; }
        public BaseValidator Validator { get; set; }
    }
}
