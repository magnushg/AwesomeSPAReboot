define('binder', ['jquery', 'ko', 'vm'],
    function ($, ko, vm) {

        var bind = function () {
            ko.applyBindings(vm.shell, getView("#main-view"));
            ko.applyBindings(vm.images, getView("#home-view"));
            ko.applyBindings(vm.stats, getView("#stats-view"));
        },

            getView = function (viewName) {
                return $(viewName).get(0);
            };

        return {
            bind: bind
        };
    });