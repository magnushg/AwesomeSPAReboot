using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using AwesomeSPAReboot.Models;
using Newtonsoft.Json;

namespace AwesomeSPAReboot.Services
{
    public interface IImagesService
    {
        IEnumerable<ImageData> GetImagesFromTag(string searchTerm);
    }

    public class ImagesService : IImagesService
    {
        private readonly IConfigurationProvider _configurationProvider;

        public ImagesService(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public IEnumerable<ImageData> GetImagesFromTag(string searchTerm)
        {
            var address = string.Format(_configurationProvider.GetConfigurationFor("SearchTagUrl"), searchTerm);
            var client = new WebClient {Encoding = Encoding.UTF8};
            var data = client.DownloadString(address);
            var deserializedData = JsonConvert.DeserializeObject<InstagramData>(data);
            var instagramData = deserializedData.data.Select(imageData => new ImageData
                                                                      {
                                                                          caption = imageData.caption != null ? imageData.caption.text : "",
                                                                          user = imageData.user != null ? imageData.user.username : "",
                                                                          userProfilePicture = imageData.user != null ? imageData.user.profile_picture : "",
                                                                          userRealName = imageData.user != null ? imageData.user.full_name : "",
                                                                          link = imageData.link,
                                                                          image_standard_res = imageData.images.standard_resolution.url,
                                                                          likes = imageData.likes.count,
                                                                          tags = imageData.tags
                                                                      });
            
            return instagramData;
        }
    }
}
