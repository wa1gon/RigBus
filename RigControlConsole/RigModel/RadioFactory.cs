using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RadioFactory
    {
        static public IRigModel Get(string radioType)
        {
            IRigModel newRig;
            switch(radioType.ToLower())
            {
                case "dummy":
                    newRig = new DummyRig();
                    return newRig;

            }
            return null;
        }
    }
}
