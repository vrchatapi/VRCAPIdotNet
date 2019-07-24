using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using VRCAPIdotNet.VRCAPI;
using static VRCAPIdotNet.VRCAPI.Dependencies;

namespace VRCAPIdotNet.VRCAPI.Endpoints
{
    public class Auth
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Auth(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public async Task<UserSelfRES> Login()
        {
            string json = "";

            UserSelfRES userSelfRES = null;

            HttpResponseMessage res = await httpClient.GetAsync($"auth/user{apiKey}");
            json = await res.Content.ReadAsStringAsync();

            if (res.IsSuccessStatusCode)
            {
                userSelfRES = JsonConvert.DeserializeObject<UserSelfRES>(json);
                currentUser = userSelfRES;
                inErrorState = false;

                Console.WriteLine($"{userSelfRES.id}");
                Console.WriteLine($"Logged in as: {userSelfRES.displayName}");
                Console.WriteLine("");
            }
            else if (res.StatusCode == HttpStatusCode.Forbidden)
            {
                AuthModerationsRES authModerationsRES = JsonConvert.DeserializeObject<AuthModerationsRES>(json);
                inErrorState = true;

                if (json.ToLower().Contains("temporary ban"))
                {
                    Console.WriteLine($"Status: {res.StatusCode} --- {res.ReasonPhrase}");
                    Console.WriteLine($"message: {authModerationsRES.error["message"]}");
                    Console.WriteLine($"status_code: {authModerationsRES.error["status_code"]}");
                    Console.WriteLine();
                    Console.WriteLine($"Looks like you are banned!");
                    Console.WriteLine($"{authModerationsRES.target}");
                    Console.WriteLine($"{authModerationsRES.reason}");
                    Console.WriteLine($"{authModerationsRES.expires}");
                    Console.WriteLine();
                    isBanned = true;
                }
                else
                {
                    Console.WriteLine($"Status: {res.StatusCode} --- {res.ReasonPhrase}");
                    Console.WriteLine($"message: {authModerationsRES.error["message"]}");
                    Console.WriteLine($"status_code: {authModerationsRES.error["status_code"]}");
                    Console.WriteLine();
                }
            }
            else if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                BasicRES basicRES = JsonConvert.DeserializeObject<BasicRES>(json);
                Console.WriteLine($"Status: {res.StatusCode} --- {res.ReasonPhrase}");
                Console.WriteLine($"message: {basicRES.error["message"]}");
                Console.WriteLine($"status_code: {basicRES.error["status_code"]}");
                Console.WriteLine();
                inErrorState = true;
            }
            else
            {
                inErrorState = true;
                Console.WriteLine($"Unexpected Error! --- {res.StatusCode} --- {res.ReasonPhrase}");
                Console.WriteLine();
            }

            return userSelfRES;
        }

        [Obsolete("This no longer works! VRChat implemented a captcha check.")]
        public async Task<UserSelfRES> Register(string username, string password, string email, string birthday = null, string acceptedTOSVersion = null)
        {
            JObject json = new JObject();
            json["username"] = username;
            json["password"] = password;
            json["email"] = email;

            if (!string.IsNullOrEmpty(birthday))
                json["birthday"] = birthday;

            if (!string.IsNullOrEmpty(acceptedTOSVersion))
                json["acceptedTOSVersion"] = acceptedTOSVersion;

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"auth/register{apiKey}", content);

            UserSelfRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<UserSelfRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Registered as: {res.displayName}");
                Console.WriteLine();
            }
            else
            {
                inErrorState = true;
            }

            return res;
        }

        [Obsolete("This no longer works! VRChat implemented a captcha check.")]
        public async Task RegisterV(string username, string password, string email, string birthday = null, string TOSVersion= null)
        {
            JObject json = new JObject();
            json["username"] = username;
            json["password"] = password;
            json["email"] = email;

            if (!string.IsNullOrEmpty(birthday))
                json["birthday"] = birthday;

            if (!string.IsNullOrEmpty(TOSVersion))
                json["acceptedTOSVersion"] = TOSVersion;

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"auth/register{apiKey}", content);
            var yayeet = await response.Content.ReadAsStringAsync();

            Console.WriteLine(yayeet);
            Console.WriteLine();
            Console.WriteLine(response.StatusCode + " --- " + response.ReasonPhrase);
            Console.WriteLine();
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine();
            Console.WriteLine(response.RequestMessage.ToString());
        }

        public async Task<UserSelfRES> UpdateInfo(string userId, string email = null, string birthday = null, string acceptedTOSVersion = null, List<string> tags = null)
        {
            JObject json = new JObject();

            if (email != null)
                json["email"] = email;

            if (birthday != null)
                json["birthday"] = birthday;

            if (acceptedTOSVersion != null)
                json["acceptedTOSVersion"] = acceptedTOSVersion;

            if (tags != null)
                json["tags"] = JToken.FromObject(tags);

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PutAsync($"users/{userId}{apiKey}", content);

            UserSelfRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<UserSelfRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Updated info from: {res.displayName}");
                Console.WriteLine();
            }

            return res;
        }
    }
}
