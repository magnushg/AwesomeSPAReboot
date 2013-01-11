define('hubs.updateHub', [], function() {
    var updater = $.connection.updateHub;
    setup = function() {
        $.connection.hub.start().done(function() {
        });
    },
    update = function(callback) {
        updater.client.update = function(message) {
            callback(message);
        };
    },
    listen = function(term) {
        updater.server.listenToSearch(term);
    };
    
    return {
        setup: setup,
        update: update,
        listen: listen
    };
});