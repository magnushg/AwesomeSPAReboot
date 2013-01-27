define('bootstrapper', ['ko', 'binder', 'toastr', 'hubs.updateHub', 'vm'], function(ko, binder, toastr, updateHub, vm) {
    var run = function() {

        $.when(updateHub.setup())
            .done(vm.shell.init())
            .done(binder.bind());
            
        
        toastr.options.positionClass = 'toast-bottom-right';
        toastr.success('Application loaded', 'Message');
    };
    
    return {
        run: run
    };
})