﻿export function showReport(reportContainer, accessToken, embedUrl, embedReportId) {
    // Get models. models contains enums that can be used.
    var models = window['powerbi-client'].models;
    var config = {
        type: 'report',
        tokenType: models.TokenType.Embed,
        accessToken: accessToken,
        embedUrl: embedUrl,
        id: embedReportId,
        permissions: models.Permissions.All,
        settings: {
            filterPaneEnabled: false,
            navContentPaneEnabled: false
        }
    };
    // Embed the report and display it within the div container.
    powerbi.embed(reportContainer, config);
}

export function showAlert(message) {
    alert(message);
}