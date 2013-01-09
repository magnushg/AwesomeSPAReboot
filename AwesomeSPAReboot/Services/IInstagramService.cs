using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using AwesomeSPAReboot.Models;
using Newtonsoft.Json;

namespace AwesomeSPAReboot.Services
{
    public interface IInstagramService
    {
        IEnumerable<InstagramBasicData> GetImagesFromTag(string searchTerm);
    }

    public class InstagramService : IInstagramService
    {
        public InstagramService()
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
                                                                          Caption = d.caption != null ? d.caption.text : "",
                                                                          User = d.user != null ? d.user.username : "",
                                                                          Link = d.link,
                                                                          Image_standard_res = d.images.standard_resolution.url,
                                                                          Likes = d.likes.count
                                                                      });
            
            return instagramData;
        }
    }

    public class InstagramBasicData
    {
        public string Caption { get; set; }
        public string User { get; set; }
        public string Link { get; set; }
        public string Image_standard_res { get; set; }
        public int Likes { get; set; }
    }
}
