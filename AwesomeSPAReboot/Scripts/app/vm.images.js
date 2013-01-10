define('vm.images', ['ko', 'jquery', 'underscore','dataservice.images', 'model'], function (ko, $, _, dataservice, model) {
    var images = ko.observableArray(),
        loading = ko.observable(false),
        searchTerm = ko.observable(),
        performSearch = function() {
            toastr.success(searchTerm());
            dataservice.getImages({ success: function (data) { images(data); }, error: function () { } }, searchTerm());
        },
        init = function() {
            loading(true);
            console.log('loading data...');
            $.when(dataservice.getImages(
                {
                    success: function (data) {
                        var imageFeed = _.map(data, function(feed) {
                            return new model.ImageFeed()
                                .caption(feed.caption)
                                .user(feed.user)
                                .link(feed.link)
                                .image_standard_res(feed.image_standard_res)
                                .likes(feed.likes);
                        });
                        images(imageFeed);
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
        loading: loading,
        searchTerm: searchTerm,
        performSearch: performSearch
    };
});