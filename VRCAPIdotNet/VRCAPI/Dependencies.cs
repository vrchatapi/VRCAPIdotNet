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

namespace VRCAPIdotNet.VRCAPI
{
    public static class Dependencies
    {
        public static HttpClient httpClient = null;
        public static UserSelfRES currentUser = null;
        public static string baseAddress = "https://api.vrchat.cloud/api/1/";
        public static string currentApiKey = "JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26";
        public static string apiKey = $"?apiKey={currentApiKey}";

        public static bool inErrorState = false;
        public static bool isBanned = false;
    }
}
