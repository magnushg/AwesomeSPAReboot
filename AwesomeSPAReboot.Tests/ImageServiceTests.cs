using System.Collections.Generic;
using System.Linq;
using AwesomeSPAReboot.Models.Contracts;
using AwesomeSPAReboot.Services;
using NUnit.Framework;

namespace AwesomeSPAReboot.Tests
{
    [TestFixture]
    public class ImageServiceTests
    {
        private IImagesService _imagesService;
        
        [SetUp]
        public void Setup()
        {
            _imagesService = new ImagesService(new ConfigurationProvider(new Dictionary<string, string>
                                                                                           {
                                                                                               {
                                                                                                   AppConstants.SEARCH_TAG_URL_KEY,
                                                                                                   "https://api.instagram.com/v1/tags/{0}/media/recent?access_token=24613827.f59def8.557cc0f5848b4738b417ef677d2ced5a"
                                                                                               },
                                                                                               {
                                                                                                   AppConstants.SEARCH_USERNAME_URL_KEY,
                                                                                                   "https://api.instagram.com/v1/users/search?q={0}&access_token=24613827.f59def8.557cc0f5848b4738b417ef677d2ced5a"
                                                                                               },
                                                                                               {
                                                                                                   AppConstants.SEARCH_USER_IMAGES_URL_KEY,
                                                                                                   "https://api.instagram.com/v1/users/{0}/media/recent/?access_token=24613827.f59def8.557cc0f5848b4738b417ef677d2ced5a"
                                                                                               }
                                                                                           }));
        }

        [Test]
        public void When_requesting_images_without_search_type_can_return_image_feed()
        {
            var result = _imagesService.GetImages("cat");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Any());
        }

        [Test]
        public void When_requesting_images_with_search_type_tag_can_return_image_feed()
        {
            var result = _imagesService.GetImages("#cat");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Any());
        }

        [Test]
        public void When_requesting_images_with_search_type_user_can_return_image_feed()
        {
            var result = _imagesService.GetImages("@magnushg");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Any());
        }

        [Test]
        public void When_requesting_images_with_search_type_user_with_silly_non_existing_username_can_return_empty_image_feed()
        {
            var result = _imagesService.GetImages("@abracadabrafromspacemonkey");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.LessThan(1));
        }

    }
}
