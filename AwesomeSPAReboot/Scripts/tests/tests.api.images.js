///<reference path="tests.config.js"/>

require(['ajaxhelper'], function (ajax) {
    test("images api returns data", function () {
        var result;
        ajax.ajaxRequest("api/images").done(function (data) {
            result = data;
            ok(true, result.length > 0);
        });
    });
});