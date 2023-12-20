using System;
using System.Collections.Generic;

namespace FhirBlaze.CDSHooks
{
    public class CDSCard
    {
        public string uuid;
        public string summary;
        public string detail; //Optional
        public string indicator;
        public CDSSource source;

        public List<CDSLink> links;

        public List<CDSCoding> overrideReasons;
    }
}

