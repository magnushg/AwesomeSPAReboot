define('hubs.updateHub', [], function() {
    var updater = $.connection.updateHub;
    setup = function() {
        $.connection.hub.start().done(function() {
            console.log('SignalR ready!');
        });
    },
    update = function(callback) {
        updater.client.update = function(message) {
            callback(message);
        };
    },
    updateSearchTerms = function (callback) {
        updater.client.updateSearchTerms = function (message) {
            callback(message);
        };
    },
    subscribe = function(subscribe, term, freq) {
        subscribe ? updater.server.listenToSearch(term, freq) : updater.server.unsubscribe();
    };
    
    
    return {
        setup: setup,
        update: update,
        updateSearchTerms: updateSearchTerms,
        subscribe: subscribe,
        
    };
});