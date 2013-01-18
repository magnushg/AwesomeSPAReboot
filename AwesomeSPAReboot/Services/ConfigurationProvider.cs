using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace AwesomeSPAReboot.Services
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly Dictionary<string, string> _config = new Dictionary<string, string>();

        public ConfigurationProvider()
            : this(new Dictionary<string, string> {{"SearchTagUrl", ConfigurationManager.AppSettings["InstagramAPISearchHashtag"] ?? string.Empty}})
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