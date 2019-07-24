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
using Newtonsoft.Json.Converters;

using VRCAPIdotNet.VRCAPI.Endpoints;
using VRCAPIdotNet.VRCAPI.Responses;
using VRCAPIdotNet.VRCAPI;

namespace VRCAPIdotNet.VRCAPI.Responses
{
    public class WorldRES
    {
        public string id { get; set; }
        public string name { get; set; }
        public string authorName { get; set; }
        public int totalLikes { get; set; }
        public int totalVisits { get; set; }
        public string imageUrl { get; set; }
        public string thumbnailImageUrl { get; set; }
        public bool isSecure { get; set; } // Unknown
        [JsonConverter(typeof(StringEnumConverter))]
        public ReleaseStatus releaseStatus { get; set; }
        public string organization { get; set; } // Unknown
        public int occupants { get; set; }
    }
}
