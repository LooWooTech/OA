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

        public static UserIdentity Convert(string token)
        {
            var ticket = FormsAuthentication.Decrypt(token);
            if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
            {
                return ticket.Name.ToObject<UserIdentity>();
            }
            return Anonymouse;
        }

        public string GetToken()
        {
            var tokenValue = this.ToJson();
            var ticket = new FormsAuthenticationTicket(tokenValue, true, int.MaxValue);
            FormsAuthentication.Encrypt(ticket);
            return ticket.Name;
        }
    }
}
