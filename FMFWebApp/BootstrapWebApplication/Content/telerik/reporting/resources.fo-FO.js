; (function (trv, $) {
    "use strict";

    var sr = {
        controllerNotInitialized: 'Controller is not initialized.',
        noReportInstance: 'No report instance.',
        missingTemplate: 'ReportViewer template is missing. Please specify the templateUrl in the options.',
        noReport: 'No report.',
        noReportDocument: 'No report document.',
        invalidParameter: 'Please input a valid value.',
        invalidDateTimeValue: 'Please input a valid date.',
        parameterIsEmpty: 'Parameter value cannot be empty.',
        cannotValidateType: 'Cannot validate parameter of type {type}.',
        loadingFormats: 'Loading...',
        loadingReport: 'Loading report...',
        preparingDownload: 'Preparing document to download. Please wait...',
        preparingPrint: 'Preparing document to print. Please wait...',
        errorLoadingTemplates: 'Error loading the report viewer\'s templates.',
        loadingReportPagesInProgress: '{0} pages loaded so far ...',
        loadedReportPagesComplete: 'Done. Total {0} pages loaded.',
        noPageToDisplay: "No page to display.",
        errorDeletingReportInstance: 'Error deleting report instance: {0}',
        errorRegisteringViewer: 'Error registering the viewer with the service.',
        noServiceClient: 'No serviceClient has been specified for this controller.',
        errorRegisteringClientInstance: 'Error registering client instance',
        errorCreatingReportInstance: 'Error creating report instance (Report = {0})',
        errorCreatingReportDocument: 'Error creating report document (Report = {0}; Format = {1})',
        unableToGetReportParameters: "Unable to get report parameters",
    };

    trv.sr = $.extend(trv.sr, sr);

}(window.telerikReportViewer = window.telerikReportViewer || {}, jQuery));