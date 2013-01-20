define('bootstrapper', ['ko', 'binder', 'toastr'], function(ko, binder, toastr) {
    var run = function() {

        binder.bind();
        toastr.options.positionClass = 'toast-bottom-right';
        toastr.success('Application loaded', 'Message');
    };
    
    return {
        run: run
    };
})