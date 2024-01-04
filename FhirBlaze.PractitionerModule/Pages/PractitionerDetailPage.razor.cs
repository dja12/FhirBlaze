using FhirBlaze.SharedComponents.Models;
using FhirBlaze.SharedComponents.Services;
using FhirBlaze.Models;
using Hl7.Fhir.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Task = System.Threading.Tasks.Task;
using Microsoft.Graph;
using Notification = FhirBlaze.SharedComponents.Models.Notification;
using System.Collections.Generic;
using FhirBlaze.SharedComponents;

namespace FhirBlaze.PractitionerModule.Pages
{
    [Authorize]
    public partial class PractitionerDetailPage
	{
		[Inject]
		private IFhirService FhirService { get; set; }

        [Inject]
        private NotificationService NotificationService { get; set; }

        [Inject]
        private AppState AppState { get; set; }

        [Inject]
        private FhirBlaze.SharedComponents.GraphClientFactory GraphClientFactory { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Parameter]
		public string Id { get; set; }

        private Practitioner SelectedPractitioner { get; set; } = new Practitioner();

		protected override async Task OnParametersSetAsync()
		{
			if (this.Id != null)
			{
				this.SelectedPractitioner = await FhirService.GetResourceByIdAsync<Practitioner>(this.Id);
                if (this.SelectedPractitioner != null) { 
                    if (this.SelectedPractitioner.Address != null) {
                        foreach (var x in this.SelectedPractitioner.Address)
                        {
                            if (x.Period == null) { x.Period = new Period(); }
                        }                
                    }
                    if (this.SelectedPractitioner.Name != null)
                    {
                        foreach (var x in this.SelectedPractitioner.Name)
                        {
                            if (x.Period == null) { x.Period = new Period(); }
                        }
                    }
                    if (this.SelectedPractitioner.Telecom != null)
                    {
                        foreach (var x in this.SelectedPractitioner.Telecom)
                        {
                            if (x.Period == null) { x.Period = new Period(); }
                        }
                    }
                }
            }
			else
			{
				this.SelectedPractitioner = new Practitioner() { Active = true };
			}
        }

        private async Task SavePractitioner((Practitioner practitioner, bool isEHRUser)args)
        {
            Practitioner persistedPractitioner = new Practitioner();
            try
            {
                if (string.IsNullOrEmpty(args.practitioner.Id))
                {
                    persistedPractitioner = await FhirService.CreatePractitionersAsync(args.practitioner);
                }
                else
                {
                    persistedPractitioner = await FhirService.UpdatePractitionerAsync(args.practitioner.Id, args.practitioner);
                }

                if (args.isEHRUser)
                {
                    // Update fhirUser extension in AAD for signed in user.
                    if (string.IsNullOrEmpty(AppState.PractitionerId))
                    {
                        // User is not currently associated to a practitioner, so update user now.

                        await UpdateFhirUserAttributeForUser(persistedPractitioner.Id);

                        //NotificationService.AddNotification(new Notification
                        //{
                        //    Title = "Practitioner ID Update",
                        //    Message = $"User will be link to Practitioner Id = {persistedPractitioner.Id}",
                        //    CreatedAt = DateTime.Now
                        //});
                    }
                    else
                    {
                        if (!AppState.PractitionerId.Equals(persistedPractitioner.Id))
                        {
                            // User is already associated to a practitioner and it's not the one
                            // just created/updated.  In other words, the user wants to change practitioners.

                            await UpdateFhirUserAttributeForUser(persistedPractitioner.Id);

                            //NotificationService.AddNotification(new Notification
                            //{
                            //    Title = "Practitioner ID Update",
                            //    Message = $"User will be changed from Practitioner Id {AppState.PractitionerId} to Practitioner Id = {persistedPractitioner.Id}",
                            //    CreatedAt = DateTime.Now
                            //});
                        }
                        // Else the user is already associated to the created/updated practitioner, so no-op.
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                NotificationService.AddNotification(new Notification
                {
                    Title = "FHIR Request Failed",
                    Message = $"{ex.Message}",
                    CreatedAt = DateTime.Now
                });

            }

            this.SelectedPractitioner = persistedPractitioner;

            NavigationManager.NavigateTo("/practitioners");
        }

        private async Task UpdateFhirUserAttributeForUser(string id)
        {
            var serviceClient = GraphClientFactory.GetAuthenticatedClient();
            var body = new Microsoft.Graph.User()
            {
                AdditionalData = new Dictionary<string, object>
                {
                    { GraphUserExtensions.FhirUser, $"Practitioner/{id}" }
                }

            };
            await serviceClient.Me.Request().UpdateAsync(body);

            // Update the value in AppState.
            AppState.PractitionerId = id;
        }
    }
}
