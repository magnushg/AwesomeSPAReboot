define('binder', ['jquery', 'ko', 'vm'],
    function ($, ko, vm) {

        var bind = function () {
            ko.applyBindings(vm.images, getView("#main-view"));
            },

            getView = function (viewName) {
                return $(viewName).get(0);
            };

        return {
            bind: bind
        };
    });