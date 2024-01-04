﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FhirBlaze.Data
{
    public static class Interop
    {
        internal static ValueTask CreateReport(
             IJSRuntime jsRuntime,
             ElementReference reportContainer,
             string accessToken,
             string embedUrl,
             string embedReportId)
        {
            //return jsRuntime.InvokeAsync<object>(
            return jsRuntime.InvokeVoidAsync(
                "showReport",
                reportContainer, accessToken, embedUrl, embedReportId);
        }

    }
}
