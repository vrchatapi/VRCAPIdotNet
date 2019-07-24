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
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using VRCAPIdotNet.VRCAPI;
using static VRCAPIdotNet.VRCAPI.Dependencies;

namespace VRCAPIdotNet.VRCAPI.Endpoints
{
    public class Friends
    {
        public async Task<List<UserRES>> Get(int offset = 0, int count = 20, bool offline = false)
        {
            Universal universal = new Universal();
            List<UserRES> userRESList = await universal.GET<List<UserRES>>("auth/user/friends", true, $"&offset={offset}&count={count}&offline={offline.ToString().ToLowerInvariant()}");

            Console.WriteLine($"Grabbed {userRESList.Count} friends!");
            Console.WriteLine();

            return userRESList;
        }
        public async Task<List<UserRES>> GetAll()
        {
            Universal universal = new Universal();
            var friends = new List<UserRES>();
            var offset = 0;var step = 100;var pages = 15;
            for (int i = 0; i < pages; i++)
            {
                // Console.WriteLine($"{i} Getting online friends {offset} to {offset + step}");
                var friends_part = await universal.GET<List<UserRES>>("auth/user/friends", true, $"&offset={offset}&count={step}&offline=false");
                if (friends_part == null) break;
                friends.AddRange(friends_part); // .Select(c => { c.Offline = true; return c; }));
                // Console.WriteLine($"{i} Got {friends_part.Count} online friends");
                if (friends_part.Count < 100) break;
                offset += 100;
            }
            offset = 0;
            for (int i = 0; i < pages; i++)
            {
                // Console.WriteLine($"{i} Getting offline friends {offset} to {offset + step}");
                var friends_part = await universal.GET<List<UserRES>>("auth/user/friends", true, $"&offset={offset}&count={step}&offline=true");
                if (friends_part == null) break;
                friends.AddRange(friends_part); // .Select(c => { c.Offline = true; return c; }));
                // Console.WriteLine($"{i} Got {friends_part.Count} offline friends");
                if (friends_part.Count < 100) break;
                offset += 100;
            }
            Console.WriteLine($"Downloaded list of {friends.Count} friends");
            return friends;
        }

        public async Task<NotificationRES> SendFriendRequest(string userId, string fromWho)
        {
            JObject json = new JObject();
            json["type"] = "friendrequest";
            json["message"] = $"{fromWho} wants to be your friend";

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"user/{userId}/notification{apiKey}", content);

            NotificationRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<NotificationRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Sent friendrequest to: {res.receiverUserId}");
                Console.WriteLine();
            }

            return res;
        }

        public async Task<BasicRES> DeleteFriend(string userId)
        {
            HttpResponseMessage response = await httpClient.DeleteAsync($"auth/user/friends/{userId}{apiKey}");

            BasicRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<BasicRES>(json);

                Console.WriteLine($"message: {res.success["message"]}");
                Console.WriteLine($"status_code: {res.success["status_code"]}");
                Console.WriteLine();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<BasicRES>(json);

                Console.WriteLine($"message: {res.error["message"]}");
                Console.WriteLine($"status_code: {res.error["status_code"]}");
                Console.WriteLine();
            }

            return res;
        }

        public async Task<BasicRES> AcceptFriend(string userId)
        {
            BasicRES res = null;

            HttpResponseMessage response = await httpClient.PutAsync($"auth/user/notifications/{userId}/accept{apiKey}", new StringContent(""));

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<BasicRES>(json);

                Console.WriteLine($"message: {res.success["message"]}");
                Console.WriteLine($"status_code: {res.success["status_code"]}");
                Console.WriteLine();
            }
            else
            {
                var json = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<BasicRES>(json);

                Console.WriteLine($"message: {res.error["message"]}");
                Console.WriteLine($"status_code: {res.error["status_code"]}");
                Console.WriteLine();
            }

            return res;
        }
    }
}
