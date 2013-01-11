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
        IEnumerable<InstagramBasicData> GetImagesFromTag(string searchTerm);
    }

    public class ImagesService : IImagesService
    {
        public ImagesService()
        {
        }

        public IEnumerable<InstagramBasicData> GetImagesFromTag(string searchTerm)
        {
            var address = string.Format("https://api.instagram.com/v1/tags/{0}/media/recent?access_token=24613827.f59def8.557cc0f5848b4738b417ef677d2ced5a", searchTerm);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            var data = client.DownloadString(address);
            var deserializedData = JsonConvert.DeserializeObject<InstagramData>(data);
            var instagramData = deserializedData.data.Select(d => new InstagramBasicData
                                                                      {
                                                                          caption = d.caption != null ? d.caption.text : "",
                                                                          user = d.user != null ? d.user.username : "",
                                                                          link = d.link,
                                                                          image_standard_res = d.images.standard_resolution.url,
                                                                          likes = d.likes.count
                                                                      });
            
            return instagramData;
        }
    }

    public class InstagramBasicData
    {
        public string caption { get; set; }
        public string user { get; set; }
        public string link { get; set; }
        public string image_standard_res { get; set; }
        public int likes { get; set; }
    }
}
