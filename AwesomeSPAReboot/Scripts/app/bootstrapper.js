define('bootstrapper', ['ko', 'binder', 'toastr'], function(ko, binder, toastr) {
    var run = function() {

        binder.bind();
        toastr.success('Application loaded', 'Message');
        //console.log('application started...');
    };
    
    return {
        run: run
    };
})