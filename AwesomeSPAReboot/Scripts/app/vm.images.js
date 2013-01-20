define('vm.images', ['ko', 'jquery', 'underscore','dataservice.images', 'mappers.imagesMapper', 'hubs.updateHub'], function (ko, $, _, dataservice, mapper, updateHub) {
    var images = ko.observableArray(),
        loading = ko.observable(false),
        searchTerm = ko.observable(),
        updates = ko.observable(0),
        subscribe = ko.observable(false),
        updateFreq = ko.observable(20),
        updatesText = ko.computed(function() {
            return 'updated ' + updates() + ' times';
        }),
        currentImage = ko.observable(),
        selectedTags = ko.observableArray([{ tag: '' }]),
        setCurrentImage = function (image) {
            currentImage(image);
        },
        setupTagFilterList = function () {
            return _.chain(images())
                .map(function(image) {
                    return image.tags();
                })
                .flatten()
                .uniq()
                .map(function(tag) {
                    return { tag: tag };
                })
                .value();
        },
        searchCallbacks = {
            success: function (data) {
                images(mapper.map(data));
                selectedTags(setupTagFilterList());
                subscriptionCheck();
                updates(0);
            },
            error: function (err) {
                console.log(err);
            }
        },
        performSearch = function() {
            dataservice.getImages(searchCallbacks, searchTerm());
        },
        subscriptionCheck = function() {
            updateHub.subscribe(subscribe(), searchTerm(), updateFreq());
            subscribe() && showSubscriptionAddedMessage();
        },
        toggleAutomaticUpdates = function () {
            subscribe(!subscribe());
        },
        showSubscriptionAddedMessage = function() {
            toastr.success('Added subscription for search term #' + searchTerm() + ' every ' + updateFreq() + ' secs');
        },
        init = function() {
            searchTerm('cat');
            loading(true);

            $.when(
                dataservice.getImages(searchCallbacks, searchTerm()
            )).then(function() {
                loading(false);
            });
            setupHub();
        },
        setupHub = function() {
            updateHub.setup();
            updateHub.update(function (message) {
                var feed = JSON.parse(message);
                images(mapper.map(feed));
                updates(updates() + 1);
            });
        };

    subscribe.subscribe(function() {
        subscriptionCheck();
    });
    updateFreq.subscribe(function() {
        subscriptionCheck();
    });
    
    init();
    
    return {
        images: images,
        loading: loading,
        searchTerm: searchTerm,
        performSearch: performSearch,
        updatesText: updatesText,
        subscribe: subscribe,
        updateFreq: updateFreq,
        currentImage: currentImage,
        setCurrentImage: setCurrentImage,
        selectedTags: selectedTags,
        toggleAutomaticUpdates: toggleAutomaticUpdates
    };
});