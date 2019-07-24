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
    public class Moderations
    {
        public async Task<List<ModerationsRES>> Get()
        {
            Universal universal = new Universal();
            List<ModerationsRES> moderationsRESList = await universal.GET<List<ModerationsRES>>("auth/user/moderations", true);

            Console.WriteLine($"Grabbed {moderationsRESList.Count} moderations!");
            Console.WriteLine();

            return moderationsRESList;
        }

        public async Task<List<PlayerModerationsRES>> GetPlayerModerations()
        {
            Universal universal = new Universal();
            List<PlayerModerationsRES> playerModerationsRESList = await universal.GET<List<PlayerModerationsRES>>("auth/user/playermoderations", true);

            Console.WriteLine($"Grabbed {playerModerationsRESList.Count} moderations!");
            Console.WriteLine();

            return playerModerationsRESList;
        }

        public async Task<List<PlayerModerationsRES>> GetPlayerModerated()
        {
            Universal universal = new Universal();
            List<PlayerModerationsRES> playerModeratedRESList = await universal.GET<List<PlayerModerationsRES>>("auth/user/playermoderated", true);

            Console.WriteLine($"Grabbed {playerModeratedRESList.Count} moderations!");
            Console.WriteLine();

            return playerModeratedRESList;
        }
    }
}
