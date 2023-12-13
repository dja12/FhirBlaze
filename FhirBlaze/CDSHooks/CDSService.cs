using System.Collections.Generic;

namespace FhirBlaze.CDSHooks
{
    public class CDSService
    {
        //public CDSService(CDSService other)
        //{
        //    Hook = other.Hook;
        //    Title = other.Title;
        //    Description = other.Description;
        //    Id = other.Id;
        //    prefetch = other.prefetch;
        //}

        public string Hook;
        public string Title;
        public string Description;
        public string Id;
        public Dictionary<string, string> prefetch;
    }
}