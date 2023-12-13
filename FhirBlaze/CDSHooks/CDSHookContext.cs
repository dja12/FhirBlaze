using System;
namespace FhirBlaze.CDSHooks
{
    public class CDSHookContext
    {
        // The id of the current user. Must be in the format [ResourceType]/[id].
        // For patient-view hook, the user is expected to be of type Practitioner, PractitionerRole, Patient, or
        // RelatedPerson.  Patient or RelatedPerson are appropriate when a patient or their proxy are viewing the 
        // record. For example, Practitioner/abc or Patient/123.
        // For the order-select hook, the user is expected to be of type Practitioner or PractitionerRole. For 
        // example, PractitionerRole/123 or Practitioner/abc.
        // REQUIRED
        public string userId;

        // The FHIR Patient.id of the current patient in context.
        // REQUIRED
        public string patientId;

        // The FHIR Encounter.id of the current encounter in context.
        // OPTIONAL
        public string encounterId;
    }
}

