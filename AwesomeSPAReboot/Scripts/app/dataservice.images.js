define('dataservice.images', ['ajaxhelper'], function (ajaxhelper) {
    var getImages = function (callbacks, searchTerm) {
        var search = { searchTerm: searchTerm };
        var data = search.searchTerm ? search : searchTerm;
        return ajaxhelper.ajaxRequest('api/images', data)
            .done(callbacks.success)
            .fail(callbacks.error);
    };
    return {
        getImages: getImages
    };
});
