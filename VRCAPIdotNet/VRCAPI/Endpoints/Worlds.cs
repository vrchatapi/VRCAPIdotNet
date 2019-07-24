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

using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using VRCAPIdotNet.VRCAPI;
using static VRCAPIdotNet.VRCAPI.Dependencies;

namespace VRCAPIdotNet.VRCAPI.Endpoints
{
    public class Worlds
    {
        public async Task<WorldSelfRES> GetSingle(string id)
        {
            Universal universal = new Universal();
            WorldSelfRES worldRES = await universal.GET<WorldSelfRES>("worlds/", id, true);

            worldRES.instances = worldRES._instances.Select(data => new WorldInstance()
            {
                id = (string)data[0],
                occupants = (int)data[1]
            }).ToList();

            Console.WriteLine($"{worldRES.id}");
            Console.WriteLine($"Grabbed World: ' {worldRES.name} ' made by {worldRES.authorName}");
            Console.WriteLine();

            return worldRES;
        }

        public async Task<List<WorldRES>> Get(string parameters)
        {
            Universal universal = new Universal();
            List<WorldRES> worldRESList = await universal.GET<List<WorldRES>>("worlds", true, parameters);

            Console.WriteLine($"Grabbed {worldRESList.Count} worlds!");
            Console.WriteLine();

            return worldRESList;
        }

        public async Task<List<WorldRES>> Get(WorldGroups? endpoint = null, bool? featured = null,
            SortOptions? sort = null, UserOptions? user = null,
            string userId = null, string keyword = null, string tags = null, string excludeTags = null,
            ReleaseStatus? releaseStatus = null, int offset = 0, int count = 20)
        {
            var param = new StringBuilder();
            param.Append($"&n={count}");
            param.Append($"&offset={offset}");

            if (featured.HasValue)
            {
                param.Append($"&featured={featured.Value}");

                if (featured.Value && sort.HasValue == false)
                {
                    param.Append("&sort=order");
                }
            }

            if (sort.HasValue)
            {
                param.Append($"&sort={sort.Value.ToString().ToLowerInvariant()}");

                if (sort.Value == SortOptions.Popularity && featured.HasValue == false)
                {
                    param.Append("&featured=false");
                }
            }

            if (user.HasValue)
                param.Append($"&user={user.Value.ToString().ToLowerInvariant()}");
            if (!string.IsNullOrEmpty(userId))
                param.Append($"&userId={userId}");
            if (!string.IsNullOrEmpty(keyword))
                param.Append($"&search={keyword}");
            if (!string.IsNullOrEmpty(tags))
                param.Append($"&tag={tags}");
            if (!string.IsNullOrEmpty(excludeTags))
                param.Append($"&notag={excludeTags}");
            if (releaseStatus.HasValue)
                param.Append($"&releaseStatus={releaseStatus.Value.ToString().ToLowerInvariant()}");

            string baseUrl = "worlds";
            if (endpoint.HasValue)
            {
                switch (endpoint.Value)
                {
                    case WorldGroups.Active:
                        baseUrl = "worlds/active";
                        break;
                    case WorldGroups.Recent:
                        baseUrl = "worlds/recent";
                        break;
                    case WorldGroups.Favorite:
                        baseUrl = "worlds/favorites";
                        break;
                }
            }

            Universal universal = new Universal();
            List<WorldRES> worldSelfRESList = await universal.GET<List<WorldRES>>(baseUrl, true, param.ToString());

            Console.WriteLine($"Grabbed {worldSelfRESList.Count} Worlds!");
            Console.WriteLine();

            return worldSelfRESList;
        }

        public async Task<WorldMDRES> GetMetadata(string id)
        {
            Universal universal = new Universal();
            WorldMDRES worldMDRES = await universal.GET<WorldMDRES>("worlds/", id + "/metadata", true);

            Console.WriteLine($"{worldMDRES.id}");
            Console.WriteLine($"World Metadata: {worldMDRES.metadata}");
            Console.WriteLine();

            return worldMDRES;
        }

        public async Task<WorldIRES> GetInstance(string id, string instanceId)
        {
            WorldIRES worldIRES = null;

            HttpResponseMessage response = await httpClient.GetAsync($"worlds/{id}/{instanceId}{apiKey}");

            if (response.IsSuccessStatusCode)
            {
                string text = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(text);

                worldIRES = new WorldIRES
                {
                    id = json["id"].ToString(),
                    name = json["name"].ToString(),
                    privateUsers = (json["private"] is JArray)
                        ? json["private"].Select(tk => tk.ToObject<WorldIURES>()).ToList() : null,
                    friends = (json["friends"] is JArray)
                        ? json["friends"].Select(tk => tk.ToObject<WorldIURES>()).ToList() : null,
                    users = (json["users"] is JArray)
                        ? json["users"].Select(tk => tk.ToObject<WorldIURES>()).ToList() : null,
                    hidden = (json["hidden"] == null || json["hidden"].Type == JTokenType.Null) ? null : json["hidden"].ToString(),
                    nonce = (json["nonce"] == null) ? null : json["nonce"].ToString(),
                };
            }

            Console.WriteLine($"{worldIRES.id}");
            Console.WriteLine($"Grabbed instance: {worldIRES.name} from world {id}");
            Console.WriteLine();

            return worldIRES;
        }
    }
}