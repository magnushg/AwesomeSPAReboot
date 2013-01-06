define('vm.sample', ['ko', 'jquery', 'dataservice.sample'], function(ko, $, dataservice) {
    var messages = ko.observableArray(),
        init = function() {
            dataservice.getSampleData(
                {
                    success: function (data) {
                        messages(data);
                    },
                    error: function(err) {
                        console.log(err);
                    }
                }
            );
        };
    
    init();
    
    return {
        messages: messages
    };
});