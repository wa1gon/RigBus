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
        /// <summary> Input Property of the radio to be acted upon.
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>Input/Output  Value of radio properties
        /// </summary>
        public string PropertyValue { get; set; }
        /// <summary> Status of request.  Success is "OK".  Otherwise an error message.
        /// </summary>
        public string Status { get; set; }
        /// <summary> Input:  Set by the client to control which enumerated item the command acts on.
        /// Such as vfo, VAC, receiver, antenna, etc,  Can be empty, but should not be null;
        /// </summary>
        public string EnumItemNum { get; set; }
        /// <summary> Input: Radio Id for radios that share a common serial port such as 
        /// Icoms.
        /// </summary>
        public string RadioId { get; set; }
        public BaseValidator Validator { get; set; }
    }
}
