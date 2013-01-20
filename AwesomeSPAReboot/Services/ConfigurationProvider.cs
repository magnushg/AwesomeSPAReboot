using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using AwesomeSPAReboot.Models.Contracts;

namespace AwesomeSPAReboot.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly Dictionary<string, string> _config = new Dictionary<string, string>();

        public ConfigurationProvider()
            : this(new Dictionary<string, string>
                       {
                           {AppConstants.SEARCH_TAG_URL_KEY, ConfigurationManager.AppSettings["InstagramAPISearchHashtag"]},
                           {AppConstants.SEARCH_USERNAME_URL_KEY, ConfigurationManager.AppSettings["InstagramAPISearchUsername"]},
                           {AppConstants.SEARCH_USER_IMAGES_URL_KEY, ConfigurationManager.AppSettings["InstagramAPISearchUserImages"]},
                       })
        {
        }

        internal ConfigurationProvider(Dictionary<string, string> config)
        {
            _config = config;
        }

        public string GetConfigurationFor(string config)
        {
            return _config[config];
        }
    }
}