using System;
namespace FhirBlaze.CDSHooks
{
    public class CDSCoding
    {
        public string code;
        public string system = Constants.CDS_CARD_OVERRIDE_REASON_SYSTEM;
        public string display;
    }
}

