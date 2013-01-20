using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AwesomeSPAReboot.Models.Contracts
{
    public class InstagramUserData
    {
        public Userdata[] data;
    }

    public class Userdata
    {
        public string username { get; set; }
        public string first_name { get; set; }
        public string profile_picture { get; set; }
        public string id { get; set; }
        public string last_name { get; set; }
    }
}