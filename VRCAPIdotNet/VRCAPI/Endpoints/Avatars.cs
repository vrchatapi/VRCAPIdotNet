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
using VRCAPIdotNet.VRCAPI;

namespace VRCAPIdotNet.VRCAPI.Endpoints
{
    public class Avatars
    {
        public async Task<AvatarRES> GetSingle(string id)
        {
            Universal universal = new Universal();
            AvatarRES avatarRES = await universal.GET<AvatarRES>($"avatars/", id, true);

            Console.WriteLine($"{avatarRES.id}");
            Console.WriteLine($"Grabbed Avatar: ' {avatarRES.name} ' made by {avatarRES.authorName}");
            Console.WriteLine();

            return avatarRES;
        }

        public async Task<List<AvatarRES>> Get(string parameters)
        {
            Universal universal = new Universal();
            List<AvatarRES> avatarRESList = await universal.GET<List<AvatarRES>>("avatars", true, parameters);

            Console.WriteLine($"Grabbed {avatarRESList.Count} avatars!");
            Console.WriteLine();

            return avatarRESList;
        }

        public async Task<List<AvatarRES>> Get(UserOptionsA? userOptions = null, bool? featured = null, string tag = null,
            string search = null, int? n = null, int? offset = null, OrderOptionsA? orderOptions = null, ReleaseStatusA? releaseStatus = null,
            SortOptionsA? sortOptions = null, string maxUnityVersion = null, string minUnityVersion = null, 
            string maxAssetVersion = null, string minAssetVersion = null, string platform = null)
        {
            var param = new StringBuilder();
            param.Append($"&n={n}");
            param.Append($"&offset={offset}");

            if (featured.HasValue)
            {
                param.Append($"&featured={featured.Value}");

                if (featured.Value && sortOptions.HasValue == false)
                {
                    param.Append("&sort=order");
                }
            }

            if (sortOptions.HasValue)
            {
                param.Append($"&sort={sortOptions.Value.ToString().ToLowerInvariant()}");
            }

            if (userOptions.HasValue)
                param.Append($"&user={userOptions.Value.ToString().ToLowerInvariant()}");
            if (!string.IsNullOrEmpty(tag))
                param.Append($"&tag={tag.ToLower()}");
            if (!string.IsNullOrEmpty(search))
                param.Append($"&search={search.ToLower()}");
            if (orderOptions.HasValue)
                param.Append($"&order={orderOptions.Value.ToString().ToLowerInvariant()}");
            if (releaseStatus.HasValue)
                param.Append($"&releaseStatus={releaseStatus.Value.ToString().ToLowerInvariant()}");
            if (!string.IsNullOrEmpty(maxUnityVersion))
                param.Append($"&maxUnityVersion={maxUnityVersion}");
            if (!string.IsNullOrEmpty(minUnityVersion))
                param.Append($"&minUnityVersion={minUnityVersion}");
            if (!string.IsNullOrEmpty(maxAssetVersion))
                param.Append($"&maxAssetVersion={maxAssetVersion}");
            if (!string.IsNullOrEmpty(minAssetVersion))
                param.Append($"&minAssetVersion={minAssetVersion}");
            if (!string.IsNullOrEmpty(platform))
                param.Append($"&platform={platform}");

            Universal universal = new Universal();
            List<AvatarRES> avatarRESList = await universal.GET<List<AvatarRES>>("avatars", true, param.ToString());

            Console.WriteLine($"Grabbed {avatarRESList.Count} avatars!");
            Console.WriteLine();

            return avatarRESList;
        }
    }
}
