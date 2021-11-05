using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IGIEditor
{
    class QUserInfo
    {
        static QUserInfo user_info = new QUserInfo();
        private string base_url = "http://ipinfo.io/";

        //Stop instantiation of class from external methods.
        private QUserInfo() { }

        //Properties of IpInfo structure.
        [JsonProperty("ip")]
        private string Ip { get; set; }

        [JsonProperty("city")]
        private string City { get; set; }

        [JsonProperty("region")]
        private string Region { get; set; }

        [JsonProperty("country")]
        private string Country { get; set; }

        [JsonProperty("loc")]
        private string Location { get; set; }

        [JsonProperty("postal")]
        private string Postal { get; set; }


        //Method to get Public IP address of user.
        public static string GetUserPublicIP()
        {
            string response_data = new WebClient().DownloadString(user_info.base_url);
            user_info = JsonConvert.DeserializeObject<QUserInfo>(response_data);
            return user_info.Ip;
        }

        //Method to get user city with and without IP.
        public static string GetUserCity(string ip = "")
        {
            string response_data = new WebClient().DownloadString(user_info.base_url + ip);
            user_info = JsonConvert.DeserializeObject<QUserInfo>(response_data);
            return user_info.City;
        }


        //Method to get user region with and without IP.
        public static string GetUserRegion(string ip = "")
        {
            string response_data = new WebClient().DownloadString(user_info.base_url + ip);
            user_info = JsonConvert.DeserializeObject<QUserInfo>(response_data);
            return user_info.Region;
        }


        //Method to get user country with and without IP.
        public static string GetUserCountry(string ip = "")
        {
            string response_data = new WebClient().DownloadString(user_info.base_url + ip);
            user_info = JsonConvert.DeserializeObject<QUserInfo>(response_data);
            RegionInfo region_info = new RegionInfo(user_info.Country);
            user_info.Country = region_info.EnglishName;

            return user_info.Country;
        }

        //Method to get user location with and without IP.
        public static string GetUserLocation(string ip = "")
        {
            string response_data = new WebClient().DownloadString(user_info.base_url + ip);
            user_info = JsonConvert.DeserializeObject<QUserInfo>(response_data);
            return user_info.Location;
        }

        //Method to get user postal with and without IP.
        public static string GetUserPostal(string ip = "")
        {
            string response_data = new WebClient().DownloadString(user_info.base_url + ip);
            user_info = JsonConvert.DeserializeObject<QUserInfo>(response_data);
            return user_info.Postal;
        }


        //Method to get all user informations like ip,country,geo,region etc.
        public static List<KeyValuePair<string, string>> GetUserInfo(string ip = "")
        {
            List<KeyValuePair<string, string>> user_info = new List<KeyValuePair<string, string>>() {

            //Get all the user informations and store it in list.
            new KeyValuePair<string, string>("ip_public", GetUserPublicIP()),
            new KeyValuePair<string, string>("city", GetUserCity(ip)),
            new KeyValuePair<string, string>("region",GetUserRegion(ip)),
            new KeyValuePair<string, string>("country",GetUserCountry(ip)),
            new KeyValuePair<string, string>("location",GetUserLocation(ip)),
            new KeyValuePair<string, string>("postal",GetUserPostal(ip)),
        };

            return user_info;
        }
    }
}
