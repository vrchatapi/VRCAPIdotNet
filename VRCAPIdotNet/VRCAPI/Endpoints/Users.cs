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
    public class Users
    {
        public async Task<List<UserRES>> Get(string search = null, int n = 20, int offset = 0)
        {
            string search2 = string.IsNullOrEmpty(search) ? "" : $"&search={search}";

            Universal universal = new Universal();
            List<UserRES> userRESList = await universal.GET<List<UserRES>>("users", true, $"{search2}&n={n}&offset={offset}");

            Console.WriteLine($"Grabbed {userRESList.Count} users!");
            Console.WriteLine();

            return userRESList;
        }

        public async Task<UserRES> GetById(string id)
        {
            Universal universal = new Universal();
            UserRES userRES = await universal.GET<UserRES>("users/", id, true);

            Console.WriteLine($"{userRES.id}");
            Console.WriteLine($"Grabbed User: ' {userRES.displayName} '");
            Console.WriteLine();

            return userRES;
        }

        public async Task<UserRES> GetByName(string name)
        {
            Universal universal = new Universal();
            UserRES userRES = await universal.GET<UserRES>("users/", name + "/name", true);

            Console.WriteLine($"{userRES.id}");
            Console.WriteLine($"Grabbed User: {userRES.displayName}");
            Console.WriteLine();

            return userRES;
        }
    }
}
