define('vm.images', ['ko', 'jquery', 'dataservice.images'], function (ko, $, dataservice) {
    var images = ko.observableArray(),
        loading = ko.observable(false),
        init = function() {
            loading(true);
            console.log('loading data...');
            $.when(dataservice.getImages(
                {
                    success: function (data) {
                        images(data);
                    },
                    error: function(err) {
                        console.log(err);
                    }
                }
            )).then(function() {
                loading(false);
                console.log('data loaded...');
            });
        };
    
    init();
    
    return {
        images: images,
        loading: loading
    };
});