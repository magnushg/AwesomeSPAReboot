using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AwesomeSPAReboot.Services
{
    public interface IConfigurationProvider
    {
        string GetConfigurationFor(string config);
    }
}
