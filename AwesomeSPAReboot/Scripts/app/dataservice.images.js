define('dataservice.images', ['ajaxhelper'], function (ajaxhelper) {
    var getImages = function (callbacks) {
        ajaxhelper.ajaxRequest('api/images')
            .done(callbacks.success)
            .fail(callbacks.error);
    };
    return {
        getImages: getImages
    };
});
