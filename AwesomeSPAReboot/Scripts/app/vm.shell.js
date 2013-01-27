define('vm.shell', ['ko', 'jquery', 'underscore', 'model', 'hubs.updateHub', 'vm.images'], function(ko, $, _, model, updateHub, imagesVm) {
    var recentSearches = ko.observableArray(),
        loading = ko.observable(false),
        automaticUpdates = ko.observable(false),
        updateFrequencies = ko.observableArray(['20', '40', '60', '120', '240']),
        updateFrequency = ko.observable(20),
        searchTerm = ko.observable(),
        automaticUpdatesText = ko.computed(function () {
            return automaticUpdates() ? 'Turn automatic updates off' : 'Turn automatic updates on';
        }),
        updateFrequencyText = ko.computed(function () {
            return 'Update frequency ' + updateFrequency() + ' secs';
        }),
        init = function() {
            setupHub();
            imagesVm.init();
        },
        setupHub = function() {
            updateHub.updateSearchTerms(function(message) {
                recentSearches(message);
            });
        },
        performSearch = function() {
            imagesVm.getImages(loading);
        },
        searchFor = function (term) {
            searchTerm(term);
            imagesVm.getImages(loading);
        },
        setUpdateFrequency = function(frequency) {
            updateFrequency(frequency);
        },
        toggleAutomaticUpdates = function() {
            automaticUpdates(!automaticUpdates());
        };

    searchTerm.subscribe(function(newValue) {
        imagesVm.searchTerm(newValue);
    });
    automaticUpdates.subscribe(function(newValue) {
        imagesVm.automaticUpdates(newValue);
    });
    updateFrequency.subscribe(function (newValue) {
        imagesVm.updateFrequency(newValue);
    });

    return {
        init: init,
        loading: loading,
        recentSearches: recentSearches,
        performSearch: performSearch,
        searchFor: searchFor,
        searchTerm: searchTerm,
        automaticUpdates: automaticUpdates,
        updateFrequencies: updateFrequencies,
        updateFrequency: updateFrequency,
        setUpdateFrequency: setUpdateFrequency,
        toggleAutomaticUpdates: toggleAutomaticUpdates,
        automaticUpdatesText: automaticUpdatesText,
        updateFrequencyText: updateFrequencyText
    };
})