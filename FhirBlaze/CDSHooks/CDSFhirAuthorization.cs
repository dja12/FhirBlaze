using System;
namespace FhirBlaze.CDSHooks
{
    public class CDSFhirAuthorization
    {
        // This is the OAuth 2.0 access token that provides access to the FHIR server.
        public string access_token;

        // Fixed value: Bearer
        public string token_type;

        // The lifetime in seconds of the access token.
        public int expires_in;

        // The scopes the access token grants the CDS Service.
        public string scope;

        // The OAuth 2.0 client identifier of the CDS Service, as registered with the CDS Client's authorization server.
        public string subject;
    }
}

