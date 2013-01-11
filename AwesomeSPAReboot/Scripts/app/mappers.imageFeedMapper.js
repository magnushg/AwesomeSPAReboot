define('mappers.imageFeedMapper', ['model'], function(model) {
    var map = function(d) {
        return _.map(d, function(feed) {
            return new model.ImageFeed()
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