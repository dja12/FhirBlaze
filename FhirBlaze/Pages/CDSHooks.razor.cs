using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace FhirBlaze.Pages
{
    [Authorize]
    public partial class CDSHooks
	{
        [CascadingParameter] public Task<AuthenticationState> AuthTask { get; set; }
    }
}

