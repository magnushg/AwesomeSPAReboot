namespace AwesomeSPAReboot.Models
{
    public class Pagination
    {
        public string next_max_tag_id { get; set; }
        public string deprecation_warning { get; set; }
        public string next_max_id { get; set; }
        public string next_min_id { get; set; }
        public string min_tag_id { get; set; }
        public string next_url { get; set; }
    }

    public class Meta
    {
        public int code { get; set; }
    }

    public class Location
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string name { get; set; }
        public int? id { get; set; }
    }

    public class Comments
    {
        public int count { get; set; }
        public object[] data { get; set; }
    }

    public class Likes
    {
        public int count { get; set; }
        public object[] data { get; set; }
    }

    public class Low_Resolution
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Standard_Resolution
    {
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Images
    {
        public Low_Resolution low_resolution { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Standard_Resolution standard_resolution { get; set; }
    }

    public class From
    {
        public string username { get; set; }
        public string profile_picture { get; set; }
        public string id { get; set; }
        public string full_name { get; set; }
    }

    public class Caption
    {
        public string created_time { get; set; }
        public string text { get; set; }
        public From from { get; set; }
        public string id { get; set; }
    }

    public class User
    {
        public string username { get; set; }
        public string website { get; set; }
        public string bio { get; set; }
        public string profile_picture { get; set; }
        public string full_name { get; set; }
        public string id { get; set; }
    }

    public class Datum
    {
        public object attribution { get; set; }
        public string[] tags { get; set; }
        public string type { get; set; }
        public Location location { get; set; }
        public Comments comments { get; set; }
        public string filter { get; set; }
        public string created_time { get; set; }
        public string link { get; set; }
        public Likes likes { get; set; }
        public Images images { get; set; }
        public Caption caption { get; set; }
        public bool user_has_liked { get; set; }
        public string id { get; set; }
        public User user { get; set; }
    }

    public class InstagramData
    {
        public Pagination pagination { get; set; }
        public Meta meta { get; set; }
        public Datum[] data { get; set; }
    }
}