define('vm.images', ['ko', 'jquery', 'underscore','dataservice.images', 'mappers.imageFeedMapper', 'hubs.updateHub'], function (ko, $, _, dataservice, mapper, update) {
    var images = ko.observableArray(),
        loading = ko.observable(false),
        searchTerm = ko.observable(),
        updates = ko.observable(0),
        updatesText = ko.computed(function() {
            return 'updated ' + updates() + ' times';
        });
        performSearch = function() {
            dataservice.getImages({
                success: function(data) {
                    images(mapper.map(data));
                    update.listen(searchTerm());
                    toastr.success('Added subscription for search term #' + searchTerm());
                    updates(0);
                },
                error: function () {
                    toastr.error('Failed to perform image search');
                }
            }, searchTerm());
            
        },
        init = function() {
            loading(true);
            console.log('loading data...');
            $.when(dataservice.getImages(
                {
                    success: function(data) {
                        images(mapper.map(data));
                    },
                    error: function(err) {
                        console.log(err);
                    }
                }
            )).then(function() {
                loading(false);
                console.log('data loaded...');
            });
            update.setup();
            update.update(function(message) {
                var feed = JSON.parse(message);
                images(mapper.map(feed));
                updates(updates() + 1);
            });
        };
    
    init();
    
    return {
        images: images,
        loading: loading,
        searchTerm: searchTerm,
        performSearch: performSearch,
        updatesText: updatesText
    };
});