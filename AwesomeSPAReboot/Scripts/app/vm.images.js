define('vm.images', ['ko', 'jquery', 'underscore','dataservice.images', 'mappers.imagesMapper', 'hubs.updateHub'], function (ko, $, _, dataservice, mapper, updateHub) {
    var images = ko.observableArray(),
        searchTerm = ko.observable(),
        automaticUpdates = ko.observable(false),
        updateFrequency = ko.observable(20),
        currentImage = ko.observable(),
        active = ko.observable(false),
        name = "Home",
        setCurrentImage = function (image) {
            currentImage(image);
        },
        searchCallbacks = {
            success: function(data) {
                mapAndSetImages(data);
                subscriptionCheck();
            },
            error: function(err) {
                console.log(err);
            }
        },
        searchFor = function(term) {
            searchTerm(term);
            getImages();
        },
        subscriptionCheck = function() {
            updateHub.subscribe(automaticUpdates(), searchTerm(), updateFrequency());
            automaticUpdates() && showAutomaticUpdatesMessage();
        },
        showAutomaticUpdatesMessage = function() {
            toastr.success('Activated automatic updates for search term ' + searchTerm() + ' every ' + updateFrequency() + ' secs');
        },
        getImages = function(loadingIndicator) {
            loadingIndicator && loadingIndicator(true);

            $.when(
                dataservice.getImages(searchCallbacks, searchTerm()
                )).then(function() {
                    loadingIndicator && loadingIndicator(false);
                    updateHub.publishSearches();
                });
        },
        setupHub = function() {
            updateHub.update(function(message) {
                var feed = JSON.parse(message);
                mapAndSetImages(feed);
            });
        },
        mapAndSetImages = function(data) {
            images(mapper.map(data));
        },
        init = function() {
            setupHub();
        };

    automaticUpdates.subscribe(function() {
        subscriptionCheck();
    }),
    updateFrequency.subscribe(function() {
        subscriptionCheck();
    });
    
    return {
        images: images,
        searchTerm: searchTerm,
        getImages: getImages,
        automaticUpdates: automaticUpdates,
        currentImage: currentImage,
        setCurrentImage: setCurrentImage,
        searchFor: searchFor,
        updateFrequency: updateFrequency,
        init: init,
        active: active,
        name: name
    };
});