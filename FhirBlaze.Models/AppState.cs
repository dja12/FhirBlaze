using System;
using FhirBlaze.CDSHooks;
using Hl7.Fhir.Model;

namespace FhirBlaze.Models
{
	public class AppState
	{
		// The logged in user's AAD OID.
		public string? UserId { get; set; }

		// The logged in user's AAD given name.
		public string? UserFirstName { get; set; }

		// The logged in users's AAD surname.
		public string? UserLastName { get; set; }

		// Access token used for context cache request.
		public string? AccessToken { get; set; }

		public CDSService? PatientViewService { get; set; }
		public CDSServices? CurrentServices { get; set; }

		// The logged in user's FHIR ID of the associated Practitioner.
		public string? PractitionerId { get; set; }

		public string? ResourceJson { get; set; }
		public Resource? CurrentResource { get; set; }
    }
}

