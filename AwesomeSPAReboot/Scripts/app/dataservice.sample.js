define('dataservice.sample', ['ajaxhelper'], function (ajaxhelper) {
    var getSampleData = function (callbacks) {
        ajaxhelper.ajaxRequest('api/sample')
            .done(callbacks.success)
            .fail(callbacks.error);
    };
    return {
        getSampleData: getSampleData
    };
});
