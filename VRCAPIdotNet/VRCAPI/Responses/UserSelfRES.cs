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
    public class PastDisplayName
    {
        string displayName { get; set; }
        string updated_at { get; set; }
    }

    public class UserSelfRES : UserRES
    {
        public List<PastDisplayName> pastDisplayNames { get; set; }
        public bool hasEmail { get; set; }
        public string obfuscatedEmail { get; set; }
        public bool emailVerified { get; set; }
        public bool hasBirthday { get; set; }
        public bool unsubscribe { get; set; }
        public List<string> friends { get; set; }
        public JObject blueprints { get; set; }
        public JObject currentAvatarBlueprint { get; set; }
        public List<string> events { get; set; }
        public string currentAvatar { get; set; }
        public string currentAvatarAssetUrl { get; set; }
        public int acceptedTOSVersion { get; set; }
        public JObject steamDetails { get; set; }
        public bool hasLoggedInFromClient { get; set; }
        public string authToken { get; set; }
    }
}
