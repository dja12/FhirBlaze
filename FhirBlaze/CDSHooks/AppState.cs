using System;
namespace FhirBlaze.CDSHooks
{
	public class AppState
	{
		public CDSService PatientViewService { get; set; }
		public CDSServices CurrentServices { get; set; }
		public string PractitionerId { get; set; } = "b89b6a5f-f9fe-4b63-bb37-8126ada8b855"; //TODO - allow user to set this.

    }
}

