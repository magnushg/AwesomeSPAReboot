define('bootstrapper', ['ko', 'binder'], function(ko, binder) {
    var run = function() {

        binder.bind();
        console.log('application started...');
    };
    
    return {
        run: run
    };
})