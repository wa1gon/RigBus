using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wa1gon.Models.Common;

namespace Wa1gon.Models.Validations
{
    public class BaseValidator
    {
        virtual public bool IsValid(RadioProperty setting)
        {
            return true;
        }
    }
}
