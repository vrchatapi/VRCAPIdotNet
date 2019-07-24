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
    public enum UserOptionsA
    {
        Me,
        Friends
    }

    public enum SortOptionsA
    {
        _Created_At,
        _Updated_At,
        Created,
        Updated,
        Order
    }

    public enum ReleaseStatusA
    {
        Public,
        Private,
        All,
        Hidden
    }

    public enum OrderOptionsA
    {
        Ascending,
        Descending
    }
}
