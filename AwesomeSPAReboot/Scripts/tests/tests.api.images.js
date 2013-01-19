///<reference path="tests.config.js"/>

require(['jquery'], function ($) {
    test("images api returns data", function () {
        $.ajax({
            type: 'GET',
            url: "http://localhost:53822/api/images",
            success: function(result) {
                okAsync(result.length > 0, "Api did not return result for GET request");
            },
        });
    });
});