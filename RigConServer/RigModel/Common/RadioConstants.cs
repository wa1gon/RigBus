
namespace Wa1gon.Models.Common
{
    public class RadioConstants
    {
        // Controllers
        public const string RadioController = "Radio";
        public const string InfoController = "Info";
        public const string ConnectioinController = "Connection";

        // status
        public const string Ok = "OK";

        // Commands  must be in lower case
        public const string AG = "ag";
        public const string Mode = "mode";
        public const string Freq = "freq";
        public const string ATUButton = "atub";
        public const string VerboseError = "verror";

        // VFO 
        public const string VfoA = "a";
        public const string VfoB = "b";

        // modulation modes
        public const string LSB = "LSB" ;
        public const string USB = "USB" ;
        public const string DSB = "DSB" ;
        public const string CWL = "CWL" ;
        public const string CWU = "CWU" ;
        public const string FM = "FM" ;
        public const string AM = "AM" ;
        public const string DIGU = "DIGU" ;
        public const string SPEC = "SPEC" ;
        public const string DIGL = "DIGL" ;
        public const string SAM = "SAM" ;
        public const string DRM = "DRM" ;

        // supported radios
        public const string PowerSDR = "PowerSDR";
        public const string DummyRadio = "Dummy";
        public const string Icom746 = "ICom746";


        // return codes
        public const string NotSupported = "Not Supported";
    }
}
