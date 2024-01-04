using FhirBlaze.SharedComponents.Models;
using FhirBlaze.SharedComponents.Services;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace FhirBlaze.PractitionerModule.Pages
{
    [Authorize]
    public partial class PractitionerListPage
    {
        private bool _loading = true;
        private bool Loading { get { return _loading; } }

        [Inject]
        private IFhirService FhirService { get; set; }

        [Inject]
        private NotificationService NotificationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public System.Threading.Tasks.Task<AuthenticationState> AuthTask { get; set; }

        private bool ShowSearch { get; set; } = false;

        public IList<Practitioner> Practitioners { get; set; } = new List<Practitioner>();

        protected override async Task OnInitializedAsync()
        {
            _loading = true;
            try
            {
                Practitioners = await FhirService.GetPractitionersAsync();
            }
            catch (FhirOperationException fhirEx)
            {
                Console.WriteLine("Error getting practitioner list.");
                NotificationService.AddNotification(new Notification
                {
                    Title = "FHIR Request Failed",
                    Message = $"{fhirEx.Message}",
                    CreatedAt = DateTime.Now
                });

            }
            _loading = false;
        }

        private void AddPractitionerClicked()
        {
            NavigationManager.NavigateTo("/practitioner");
        }

        private void PractitionerSelected(Practitioner practitioner)
        {
            NavigationManager.NavigateTo($"/practitioner/{practitioner.Id}");
        }

        private async Task SearchPractitioner(IDictionary<string, string> searchParameters)
        {
            try
            {
                _loading = true;
                this.Practitioners = await FhirService.SearchPractitioner(searchParameters);
                _loading = false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception");
                Console.WriteLine(e.Message); //TODO: manage the cancel search
            }
        }

    }
}
