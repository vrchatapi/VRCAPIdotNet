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

namespace VRCAPIdotNet.VRCAPI.Responses
{
    public class WorldIRES
    {
        public string id { get; set; }
        public string name { get; set; }
        [JsonProperty(PropertyName = "private")]
        public List<WorldIURES> privateUsers { get; set; }
        public List<WorldIURES> friends { get; set; }
        public List<WorldIURES> users { get; set; }
        public string hidden { get; set; }
        public string nonce { get; set; }
    }
}
