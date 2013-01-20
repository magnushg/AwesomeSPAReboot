define('vm.images', ['ko', 'jquery', 'underscore','dataservice.images', 'mappers.imagesMapper', 'hubs.updateHub'], function (ko, $, _, dataservice, mapper, updateHub) {
    var images = ko.observableArray(),
        loading = ko.observable(true),
        searchTerm = ko.observable(),
        automaticUpdates = ko.observable(false),
        updateFrequencies = ko.observableArray(['20', '40', '60', '120', '240']),
        updateFrequency = ko.observable(20),
        currentImage = ko.observable(),
        selectedTags = ko.observableArray([{ tag: '' }]),
        automaticUpdatesText = ko.computed(function() {
            return automaticUpdates() ? 'Turn automatic updates off' : 'Turn automatic updates on';
        }),
        updateFrequencyText = ko.computed(function() {
            return 'Update frequency ' + updateFrequency() + ' secs';
        }),
        setCurrentImage = function(image) {
            currentImage(image);
        },
        setupTagFilterList = function() {
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
            success: function(data) {
                mapAndSetImages(data);
                selectedTags(setupTagFilterList());
                subscriptionCheck();
            },
            error: function(err) {
                console.log(err);
            }
        },
        performSearch = function() {
            getImages();
        },
        subscriptionCheck = function() {
            updateHub.subscribe(automaticUpdates(), searchTerm(), updateFrequency());
            automaticUpdates() && showAutomaticUpdatesMessage();
        },
        toggleAutomaticUpdates = function() {
            automaticUpdates(!automaticUpdates());
        },
        showAutomaticUpdatesMessage = function() {
            toastr.success('Activated automatic updates for search term #' + searchTerm() + ' every ' + updateFrequency() + ' secs');
        },
        setUpdateFrequency = function(frequency) {
            updateFrequency(frequency);
        },
        init = function() {
            searchTerm('cat');
            getImages();
            setupHub();
        },
        getImages = function() {
            loading(true);

            $.when(
                dataservice.getImages(searchCallbacks, searchTerm()
                )).then(function () {
                    loading(false);
                });
        },
        setupHub = function() {
            updateHub.setup();
            updateHub.update(function(message) {
                var feed = JSON.parse(message);
                mapAndSetImages(feed);
            });
        },
        mapAndSetImages = function(data) {
            loading(true);
            images(data);
            loading(false);
        };

    automaticUpdates.subscribe(function () {
        subscriptionCheck();
    });
    updateFrequency.subscribe(function () {
        subscriptionCheck();
    });

    init();
    
    return {
        images: images,
        loading: loading,
        searchTerm: searchTerm,
        performSearch: performSearch,
        automaticUpdatesText: automaticUpdatesText,
        automaticUpdates: automaticUpdates,
        updateFrequencies: updateFrequencies,
        updateFrequencyText: updateFrequencyText,
        currentImage: currentImage,
        setCurrentImage: setCurrentImage,
        selectedTags: selectedTags,
        toggleAutomaticUpdates: toggleAutomaticUpdates,
        setUpdateFrequency: setUpdateFrequency
    };
});