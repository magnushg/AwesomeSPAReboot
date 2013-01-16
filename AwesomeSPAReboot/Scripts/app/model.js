define('model', ['model.image'],
    function (image) {
        var
            model = {
                Image: image
            };
        
        return model;
    });