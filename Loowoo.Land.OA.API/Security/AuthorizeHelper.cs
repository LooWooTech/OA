using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Loowoo.Land.OA.API.Security
{
    public class AuthorizeHelper
    {
        public static UserIdentity GetIdentity(HttpContext context)
        {
            var token = context.Request.Headers[AppSettings.Get("TokenKey") ?? "Token"];
            if (!string.IsNullOrWhiteSpace(token))
            {
                var ticket = FormsAuthentication.Decrypt(token);
                if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                {
                    return ticket.Name.ToObject<UserIdentity>();
                }
            }
            return UserIdentity.Anonymouse;
        }

        public static string GetToken(User user)
        {
            var tokenValue = user.ToJson();
            var ticket = new FormsAuthenticationTicket(tokenValue, true, int.MaxValue);
            return FormsAuthentication.Encrypt(ticket);
        }
    }
}