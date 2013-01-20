(function() {
    var root = this;
    require.config({
       baseUrl: "Scripts/app" 
    });
    
    defineVendorScripts();
    loadPluginsAndBoot();
    
    function defineVendorScripts() {
        define('jquery', [], function () { return root.jQuery; });
        define('ko', [], function () { return root.ko; });
        define('toastr', [], function () { return root.toastr; });
        define('underscore', [], function () { return root._; });
    }
    
    function loadPluginsAndBoot() {
        // Plugins must be loaded after jQuery and Knockout, 
        // since they depend on them.
        requirejs([
                'ko.bindingHandlers',
        ], boot);
    }
    
    function boot() {
        require(['bootstrapper'], function (bs) { bs.run(); });
    }
})();