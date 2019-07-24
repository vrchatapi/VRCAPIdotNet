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
using Newtonsoft.Json.Converters;

namespace VRCAPIdotNet.VRCAPI.Responses
{
    public enum UserOptions
    {
        Me,
        Friends,
    }

    public enum SortOptions
    {
        Popularity,
        Created,
        Updated,
        Order,
    }

    public enum ReleaseStatus
    {
        Public,
        Private,
        All,
        Hidden,
    }

    public enum WorldGroups
    {
        Any,
        Active,
        Recent,
        Favorite,
    }
}
