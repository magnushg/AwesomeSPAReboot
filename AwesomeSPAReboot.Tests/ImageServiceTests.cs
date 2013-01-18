using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AwesomeSPAReboot.Services;
using NUnit.Framework;

namespace AwesomeSPAReboot.Tests
{
    [TestFixture]
    public class ImageServiceTests
    {
        [Test]
        public void When_requesting_images_by_tag_can_return_image_feed()
        {
            IImagesService imagesService = new ImagesService(new ConfigurationProvider(new Dictionary<string, string>()
                                                                                           {
                                                                                               {
                                                                                                   "SearchTagUrl",
                                                                                                   "https://api.instagram.com/v1/tags/{0}/media/recent?access_token=24613827.f59def8.557cc0f5848b4738b417ef677d2ced5a"
                                                                                               }
                                                                                           }));

            var result = imagesService.GetImagesFromTag("cat");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Any());
        }
    }
}
