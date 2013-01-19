define('model.image', ['ko'],
    function (ko) {
        var Image = function () {
            var self = this;
            self.caption = ko.observable();
            self.user = ko.observable();
            self.userProfilePicture = ko.observable();
            self.userRealName = ko.observable();
            self.link = ko.observable();
            self.image_standard_res = ko.observable();
            self.likes = ko.observable();
            self.tags = ko.observableArray();

            self.userNameFormatted = ko.computed(function () {
                return self.user() + (self.userRealName() ? ' ( ' + self.userRealName() + ' )' : '');
            });
            return self;
        };
        return Image;
    });