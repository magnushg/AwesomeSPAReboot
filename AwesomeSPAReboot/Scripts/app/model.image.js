define('model.image', ['ko'],
    function (ko) {
        var Image = function () {
            var self = this;
            self.caption = ko.observable();
            self.user = ko.observable();
            self.link = ko.observable();
            self.image_standard_res = ko.observable();
            self.likes = ko.observable();
            return self;
        };
        return Image;
    });