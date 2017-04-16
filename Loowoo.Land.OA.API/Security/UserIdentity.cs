using Loowoo.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Loowoo.Land.OA.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Loowoo.Land.OA.API.Security
{
    [NotMapped]
    public class UserIdentity : User, System.Security.Principal.IIdentity
    {
        public static UserIdentity Anonymouse => new UserIdentity();

        [JsonIgnore]
        public string AuthenticationType => "token";

        [JsonIgnore]
        public bool IsAuthenticated => ID > 0;

        public string Name => RealName;
    }
}
