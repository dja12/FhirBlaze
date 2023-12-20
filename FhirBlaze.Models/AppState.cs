using System;
using FhirBlaze.CDSHooks;
using Hl7.Fhir.Model;

namespace FhirBlaze.Models
{
	public class AppState
	{
		public string? UserId { get; set; }
		public string? AccessToken { get; set; }
		public CDSService PatientViewService { get; set; }
		public CDSServices CurrentServices { get; set; }
		public string PractitionerId { get; set; } = "b89b6a5f-f9fe-4b63-bb37-8126ada8b855"; //TODO - allow user to set this.
		public string? ResourceJson { get; set; }
		public Resource? CurrentResource { get; set; }
    }
}

