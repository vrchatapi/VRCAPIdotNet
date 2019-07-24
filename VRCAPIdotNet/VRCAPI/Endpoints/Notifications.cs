using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class Notifications
    {
        public async Task<List<NotificationRES>> Get()
        {
            Universal universal = new Universal();
            List<NotificationRES> notificationRESList = await universal.GET<List<NotificationRES>>("auth/user/notifications", true);

            Console.WriteLine($"Grabbed {notificationRESList.Count} Notifications!");
            Console.WriteLine();

            return notificationRESList;
        }

        public async Task<NotificationRES> SendInvite(string userId, string fromWho, string worldId, string instanceId)
        {
            JObject json = new JObject();
            json["type"] = "invite";
            json["message"] = $"{fromWho} invites you to their world!";
            json["details"] = $"{worldId}:{instanceId}";

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"user/{userId}/notification{apiKey}", content);

            NotificationRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<NotificationRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Sent invite to: {res.receiverUserId}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Unexpected Error! --- {response.StatusCode} --- {response.ReasonPhrase}\n{json}");
            }

            return res;
        }

        public async Task<NotificationRES> SendMessage(string userId, string fromWho, string message)
        {
            JObject json = new JObject();
            json["type"] = "message";
            json["message"] = $"{fromWho} says: {message}";

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"user/{userId}/notification{apiKey}", content);

            NotificationRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<NotificationRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Sent message to: {res.receiverUserId}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Unexpected Error! --- {response.StatusCode} --- {response.ReasonPhrase}\n{json}");
            }

            return res;
        }

        public async Task<NotificationRES> SendVotekick(string userId, string fromWho, string selfUserId)
        {
            JObject json = new JObject();
            json["type"] = "votetokick";
            json["message"] = $"{fromWho} started a votekick!";
            json["details"] = $"{userId}, {selfUserId}";

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"user/{userId}/notification{apiKey}", content);

            NotificationRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<NotificationRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Started votekick on: {res.receiverUserId}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Unexpected Error! --- {response.StatusCode} --- {response.ReasonPhrase}\n{json}");
            }

            return res;
        }

        public async Task<NotificationRES> SendVotekickV2(string userId, string fromWho, string selfUserId)
        {
            JObject json = new JObject();
            json["type"] = "votetokick";
            json["message"] = $"{fromWho} started a votekick!";
            json["details"] = $"{userId},{selfUserId}";

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"user/{userId}/notification{apiKey}", content);

            NotificationRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<NotificationRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Started votekick on: {res.receiverUserId}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine($"Unexpected Error! --- {response.StatusCode} --- {response.ReasonPhrase}\n{json}");
            }

            return res;
        }
    }
}