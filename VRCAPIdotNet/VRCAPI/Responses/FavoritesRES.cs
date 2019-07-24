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
    public class FavoritesRES
    {
        public string id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TypeOptions typeOptions { get; set; }
        public string favoriteId { get; set; }
        public List<string> tags { get; set; }
    }
}
