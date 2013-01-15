define('model', ['model.imageFeed'],
    function (imageFeed) {
        var
            model = {
                ImageFeed: imageFeed
            };
        
        return model;
    });