define('mappers.imagesMapper', ['model'], function(model) {
    var map = function(images) {
        return _.map(images, function(feed) {
            return new model.Image()
                .caption(feed.caption)
                .user(feed.user)
                .userProfilePicture(feed.userProfilePicture)
                .userRealName(feed.userRealName)
                .link(feed.link)
                .image_standard_res(feed.image_standard_res)
                .likes(feed.likes)
                .tags(feed.tags);
        });
    };
    return {
        map: map
    };
});