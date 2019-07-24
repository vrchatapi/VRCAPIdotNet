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
    public class AvatarRES
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string authorId { get; set; }
        public string authorName { get; set; }
        public List<string> tags { get; set; }
        public string assetUrl { get; set; }
        public string imageUrl { get; set; }
        public string thumbnailImageUrl { get; set; }
        public string releaseStatus { get; set; }
        public int version { get; set; }
        public bool featured { get; set; }
        public List<UnityPackage> unityPackages { get; set; }
        public bool unityPackageUpdated { get; set; }
        public string unityPackageUrl { get; set; }
    }
}
