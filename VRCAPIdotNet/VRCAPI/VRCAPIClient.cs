using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;

using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using static VRCAPIdotNet.VRCAPI.Dependencies;

namespace VRCAPIdotNet.VRCAPI
{
    public class VRCAPIClient
    {
        public Auth Auth { get; set; }
        public Avatars Avatars { get; set; }
        public Config Config { get; set; }
        public Friends Friends { get; set; }
        public Moderations Moderations { get; set; }
        public Users Users { get; set; }
        public Worlds Worlds { get; set; }
        public Favorites Favorites { get; set; }
        public Universal Universal { get; set; }
        public Notifications Notifications { get; set; }

        public VRCAPIClient(string username, string password)
        {
            Auth = new Auth(username, password);
            Avatars = new Avatars();
            Config = new Config();
            Friends = new Friends();
            Moderations = new Moderations();
            Users = new Users();
            Worlds = new Worlds();
            Favorites = new Favorites();
            Universal = new Universal();
            Notifications = new Notifications();

            if (httpClient == null)
            {
                httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseAddress);
            }

            string base64Auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Auth.Username}:{Auth.Password}"));

            var header = httpClient.DefaultRequestHeaders;
            if (header.Contains("Authorization"))
            {
                header.Remove("Authorization");
            }
            header.Add("Authorization", $"Basic {base64Auth}");
        }
    }
}