define('vm.shell', ['ko', 'jquery', 'underscore', 'hubs.updateHub', 'vm.images', 'vm.stats'], function(ko, $, _, updateHub, imagesVm, statsVm) {
    var recentSearches = ko.observableArray(),
        loading = ko.observable(false),
        modules = ko.observableArray(),
        automaticUpdates = ko.observable(false),
        updateFrequencies = ko.observableArray(['20', '40', '60', '120', '240']),
        updateFrequency = ko.observable(20),
        searchTerm = ko.observable(),
        automaticUpdatesText = ko.computed(function() {
            return automaticUpdates() ? 'Updates on' : 'Updates';
        }),
        init = function () {
            setupHub();
            imagesVm.init();
            statsVm.init();
            registerModules();
            imagesVm.active(true);
        },
        updateFrequencyText = ko.computed(function() {
            return 'Freq. ' + updateFrequency() + ' secs';
        }),
        registerModules = function () {
            var modulesMapped = _.map([imagesVm, statsVm], function(viewModels) {
                return { name: viewModels.name, active: viewModels.active };
            });
            modules(modulesMapped);
        },
        activateModule = function(module) {
            _.each(modules(), function(m) {
                m.active(false);
            });
            var selectedModule = _.filter(modules(), function (m) {
                return m.name === module.name;
            });
            selectedModule[0].active(true);           
        },
        setupHub = function () {
            updateHub.updateSearchTerms(function (message) {
                recentSearches(message);
            });
        },
        performSearch = function() {
            imagesVm.getImages(loading);
            activateModule(imagesVm);
        },
        searchFor = function(term) {
            searchTerm(term);
            imagesVm.getImages(loading);
            activateModule(imagesVm);
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
    updateFrequency.subscribe(function(newValue) {
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
        updateFrequencyText: updateFrequencyText,
        modules: modules,
        activateModule: activateModule
    };
});