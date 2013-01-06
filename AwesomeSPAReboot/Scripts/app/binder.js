define('binder',
    ['jquery', 'ko', 'vm'],
    function ($, ko, vm) {

        bind = function () {
            ko.applyBindings(vm.sample, getView("#home-view"));
            },

            getView = function (viewName) {
                return $(viewName).get(0);
            };

        return {
            bind: bind
        };
    });