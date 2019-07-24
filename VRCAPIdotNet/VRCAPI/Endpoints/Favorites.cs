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
    public class Favorites
    {
        public async Task<FavoritesRES> GetSingle(string id)
        {
            Universal universal = new Universal();
            FavoritesRES favoritesRES = await universal.GET<FavoritesRES>("favorites/", id, true);

            Console.WriteLine($"{favoritesRES.id}");
            Console.WriteLine($"Grabbed favorite {favoritesRES.favoriteId} type of {favoritesRES.typeOptions}");
            Console.WriteLine();

            return favoritesRES;
        }

        public async Task<List<FavoritesRES>> Get()
        {
            Universal universal = new Universal();
            List<FavoritesRES> favoritesRESList = await universal.GET<List<FavoritesRES>>("favorites", true);

            Console.WriteLine($"Grabbed {favoritesRESList.Count} favorites!");
            Console.WriteLine();

            return favoritesRESList;
        }

        public async Task<FavoritesRES> New(TypeOptions? typeOptions, string favoriteId)
        {
            string type = "";
            switch (typeOptions.Value)
            {
                case TypeOptions.Avatar:
                    type = "avatar";
                    break;
                case TypeOptions.Friend:
                    type = "friend";
                    break;
                case TypeOptions.World:
                    type = "world";
                    break;
            }

            JObject json = new JObject();
            json["type"] = type;
            json["favoriteId"] = favoriteId;

            StringContent content = new StringContent(json.ToString(), Encoding.UTF8);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync($"favorites{apiKey}", content);

            FavoritesRES res = null;

            if (response.IsSuccessStatusCode)
            {
                var receivedJson = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<FavoritesRES>(receivedJson);

                Console.WriteLine($"{res.id}");
                Console.WriteLine($"Posted new favorite: {res.favoriteId}");
                Console.WriteLine();
            }

            return res;
        }
    }
}
