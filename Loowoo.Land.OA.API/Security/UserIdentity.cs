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
        public static UserIdentity Anonymouse
        {
            get
            {
                return new UserIdentity();
            }
        }

        [JsonIgnore]
        public string AuthenticationType
        {
            get { return "token"; }
        }

        [JsonIgnore]
        public bool IsAuthenticated
        {
            get { return ID > 0; }
        }
    }
}
