define('mappers.imagesMapper', ['model'], function(model) {
    var map = function(images) {
        return _.map(images, function(feed) {
            return new model.Image()
                .caption(feed.caption)
                .user(feed.user)
                .link(feed.link)
                .image_standard_res(feed.image_standard_res)
                .likes(feed.likes);
        });
    };
    return {
        map: map
    };
});