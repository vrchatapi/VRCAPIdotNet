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
using static VRCAPIdotNet.VRCAPI.Dependencies;

namespace VRCAPIdotNet.VRCAPI.Endpoints
{
    public class Config
    {
        public async Task<ConfigRES> Get()
        {
            Universal universal = new Universal();
            ConfigRES configRES = await universal.GET<ConfigRES>("config", false);

            if (string.IsNullOrEmpty(configRES.apiKey))
            {
                currentApiKey = configRES.apiKey;
                Console.WriteLine($"Changed ApiKey to: '{configRES.apiKey}'");
                Console.WriteLine();
            }
            if (currentApiKey != configRES.apiKey)
            {
                currentApiKey = configRES.apiKey;
                Console.WriteLine($"Changed ApiKey to: '{configRES.apiKey}'");
                Console.WriteLine();
            }

            Console.WriteLine($"{configRES.appName}");
            Console.WriteLine($"Current APIKey: {configRES.apiKey}");
            Console.WriteLine();

            return configRES;
        }
    }
}
