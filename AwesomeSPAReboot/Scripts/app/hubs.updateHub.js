define('hubs.updateHub', [], function() {
    var updater = $.connection.updateHub;
    setup = function() {
        return $.connection.hub.start();
    },
    update = function(callback) {
        updater.client.update = function(message) {
            callback(message);
        };
    },
    updateSearchTerms = function(callback) {
        updater.client.updateSearchTerms = function(message) {
            callback(message);
        };
    },
    subscribe = function(subscribe, term, freq) {
        subscribe ? updater.server.listenToSearch(term, freq) : updater.server.unsubscribe();
    },
    publishSearches = function() {
        updater.server.publishSearches();
    };
    
    
    return {
        setup: setup,
        update: update,
        updateSearchTerms: updateSearchTerms,
        subscribe: subscribe,
        publishSearches : publishSearches
    };
});