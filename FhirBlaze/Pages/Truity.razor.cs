using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FhirBlaze.Pages
{
    [Authorize]
    public partial class Truity
    {
        [CascadingParameter] public Task<AuthenticationState> AuthTask { get; set; }

    }
}