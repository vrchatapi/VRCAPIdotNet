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
    public class NotificationRES
    {
        public string id { get; set; }
        public string type { get; set; }
        public string senderUserId { get; set; }
        public string receiverUserId { get; set; }
        public string message { get; set; }
        public JObject details { get; set; } // unknown
        public string jobName { get; set; }
        public string jobColor { get; set; }

        [Obsolete("Typoed property, use receiverUserId instead")]
        [JsonIgnore]
        public string recieverUserId { get => receiverUserId; set => receiverUserId = value; }
    }
}
