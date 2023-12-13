using System;
using System.Collections.Generic;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Microsoft.Graph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FhirBlaze.CDSHooks
{
    public class CDSRequest
    {
        // The hook that triggered this CDS Service call.
        public string hook;

        // A UUID for this particular hook call.
        public string hookInstance;

        // The base URL of the CDS Client's FHIR server. If fhirAuthorization is provided, this field is REQUIRED. 
        // The scheme should be https.
        public string fhirServer;

        // A structure holding an OAuth 2.0 bearer access token granting the CDS Service access to FHIR resources, 
        // along with supplemental information relating to the token.
        public CDSFhirAuthorization fhirAuthorization;

        // Hook-specific contextual data that the CDS service will need.
        public CDSHookContext context;

        // The FHIR data that was prefetched by the CDS Client.
        [JsonConverter(typeof(PrefetchJsonConverter))]
        public Dictionary<string, Resource> prefetch;

        // Possible extension values including epic bpa-trigger-action.
        public dynamic extension;
    }

    public sealed class PrefetchJsonConverter : JsonConverter<Dictionary<string, Resource>>
    {
        public override Dictionary<string, Resource> ReadJson(JsonReader reader, Type objectType, Dictionary<string, Resource> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, Dictionary<string, Resource> value, JsonSerializer serializer)
        {
            if (value is null) return;
            writer.WriteStartObject();
            foreach (var (key, fhirResource) in value)
            {
                writer.WritePropertyName(key);
                var s = new FhirJsonSerializer();
                var jsonResult = s.SerializeToString(fhirResource);
                writer.WriteToken(JsonToken.Raw, jsonResult);
            }
            writer.WriteEndObject();
        }
    }
}

