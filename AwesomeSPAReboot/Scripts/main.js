(function() {
    var root = this;
    require.config({
       baseUrl: "Scripts/app" 
    });
    
    defineVendorScripts();
    boot();
    
    function defineVendorScripts() {
        define('jquery', [], function () { return root.jQuery; });
        define('ko', [], function () { return root.ko; });
        define('toastr', [], function () { return root.toastr; });
        define('underscore', [], function () { return root._; });
    }
    
    function boot() {
        require(['bootstrapper'], function (bs) { bs.run(); });
    }
})();