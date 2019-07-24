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
    public class AuthModerationsRES
    {
        public JObject error { get; set; }
        public string target{ get; set; }
        public string reason { get; set; }
        public string expires { get; set; }
    }
}
