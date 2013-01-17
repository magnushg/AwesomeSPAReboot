using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AwesomeSPAReboot.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly Dictionary<string, dynamic> _config = new Dictionary<string, dynamic>();

        public ConfigurationProvider()
        {
            _config.Add("SearchTagUrl", ConfigurationManager.AppSettings["InstagramAPISearchHashtag"] ?? string.Empty);
        }

        public string GetConfigurationFor(string config)
        {
            return _config[config];
        }
    }
}