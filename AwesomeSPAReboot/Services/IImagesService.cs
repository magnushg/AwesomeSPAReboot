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
            var instagramData = deserializedData.data.Select(image => new ImageData
                                                                      {
                                                                          caption = image.caption != null ? image.caption.text : "",
                                                                          user = image.user != null ? image.user.username : "",
                                                                          link = image.link,
                                                                          image_standard_res = image.images.standard_resolution.url,
                                                                          likes = image.likes.count
                                                                      });
            
            return instagramData;
        }
    }
}
