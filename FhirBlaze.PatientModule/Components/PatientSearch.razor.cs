using FhirBlaze.SharedComponents.Services;
using FhirBlazeNotification = FhirBlaze.SharedComponents.Models.Notification;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Components;
using System;

namespace FhirBlaze.PatientModule.Components;

public partial class PatientSearch
{
    [Parameter]
    public EventCallback<Patient> SearchPatient { get; set; } 

    [Parameter]
    public bool Processing { get; set; }

    [Parameter]
    public Models.SimplePatient Patient { get; set; }

    [Inject]
    private NotificationService NotificationService { get; set; }

    protected async void searchPatient()
    {
        try
        {
           await SearchPatient.InvokeAsync(Patient.ToHl7Patient());
        }
        catch (Exception fhirEx)
        {
            Console.WriteLine("Error getting practitioner list.");
            NotificationService.AddNotification(new FhirBlazeNotification
            {
                Title = "FHIR Request Failed",
                Message = $"{fhirEx.Message}",
                CreatedAt = DateTime.Now
            });
            // do nothing
        }
    }

}
