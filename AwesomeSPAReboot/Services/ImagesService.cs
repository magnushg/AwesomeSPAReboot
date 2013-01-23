using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using AwesomeSPAReboot.Models;
using AwesomeSPAReboot.Models.Contracts;
using Newtonsoft.Json;

namespace AwesomeSPAReboot.Services
{
    public class ImagesService : IImagesService
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly ISearchRepository _searchRepository;
        private Dictionary<string, Func<string, IEnumerable<ImageData>>> _searchTypes; 

        public ImagesService(IConfigurationProvider configurationProvider, ISearchRepository searchRepository)
        {
            _configurationProvider = configurationProvider;
            _searchRepository = searchRepository;
            _searchTypes = new Dictionary<string, Func<string, IEnumerable<ImageData>>>
                               {
                                   {"#", TagSearch},
                                   {"@", UserSearch},
                                   {"default", TagSearch}
                               };
        }

        public IEnumerable<ImageData> GetImages(string searchTerm)
        {
            var searchType = searchTerm.Substring(0, 1);
            _searchRepository.SaveSearch(new Search {Term = searchTerm, TimeStamp = DateTime.Now});

            var instagramData = _searchTypes.ContainsKey(searchType) ? _searchTypes[searchType](searchTerm.Trim(searchType.ToCharArray())) : _searchTypes["default"](searchTerm);

            return instagramData;
        }

        private IEnumerable<ImageData> UserSearch(string searchTerm)
        {
            var client = new WebClient {Encoding = Encoding.UTF8};
            var data = client.DownloadString(string.Format(_configurationProvider.GetConfigurationFor(AppConstants.SEARCH_USERNAME_URL_KEY), searchTerm));
            var deserializedData = JsonConvert.DeserializeObject<InstagramUserData>(data);
            if (deserializedData.data.Any())
            {
                return
                    SearchWithAddress(string.Format(_configurationProvider.GetConfigurationFor(AppConstants.SEARCH_USER_IMAGES_URL_KEY),
                                                    deserializedData.data.First().id));
            }
            return new List<ImageData>();
        }

        private IEnumerable<ImageData> TagSearch(string searchTerm)
        {
            return
                SearchWithAddress(string.Format(_configurationProvider.GetConfigurationFor(AppConstants.SEARCH_TAG_URL_KEY), searchTerm));
        }

        private IEnumerable<ImageData> SearchWithAddress(string address)
        {
            var client = new WebClient { Encoding = Encoding.UTF8 };
            var data = client.DownloadString(address);
            var deserializedData = JsonConvert.DeserializeObject<InstagramData>(data);
            var instagramData = deserializedData.data.Select(imageData => new ImageData
                                                                              {
                                                                                  caption =
                                                                                      imageData.caption != null
                                                                                          ? imageData.caption.text
                                                                                          : "",
                                                                                  user =
                                                                                      imageData.user != null
                                                                                          ? imageData.user.username
                                                                                          : "",
                                                                                  userProfilePicture =
                                                                                      imageData.user != null
                                                                                          ? imageData.user.profile_picture
                                                                                          : "",
                                                                                  userRealName =
                                                                                      imageData.user != null
                                                                                          ? imageData.user.full_name
                                                                                          : "",
                                                                                  link = imageData.link,
                                                                                  image_standard_res =
                                                                                      imageData.images.standard_resolution.url,
                                                                                  likes = imageData.likes.count,
                                                                                  tags = imageData.tags
                                                                              });
            return instagramData;
        }
    }
}