define('ajaxhelper', ['jquery'], function($) {
    ajaxRequest = function(url, data) {
        var options = {
            dataType: "json",
            contentType: "application/json",
            cache: false,
            type: "GET",
            data: data
        };
        return $.ajax(url, options);
    };

    return {
        ajaxRequest: ajaxRequest
    };
});