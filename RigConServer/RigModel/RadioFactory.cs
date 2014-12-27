using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RadioFactory
    {
        static public MasterBase Get(string radioType)
        {
            MasterBase newRig;
            switch(radioType.ToLower())
            {
                case "dummy":
                    newRig = new DummyMaster();
                    return newRig;
                case "flex":
                    newRig = new PowerSDRMaster();
                    return newRig;

            }
            return null;
        }
    }
}
