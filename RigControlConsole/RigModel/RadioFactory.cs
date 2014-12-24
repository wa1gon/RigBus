using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RadioFactory
    {
        static public Rig Get(string radioType)
        {
            Rig newRig;
            switch(radioType.ToLower())
            {
                case "dummy":
                    newRig = new DummyMaster();
                    return newRig;
                case "flex":
                    newRig = new FlexMaster();
                    return newRig;

            }
            return null;
        }
    }
}
