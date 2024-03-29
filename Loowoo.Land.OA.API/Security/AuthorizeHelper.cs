﻿using Loowoo.Common;
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
            var tokenKey = AppSettings.Get("TokenKey") ?? "token";
            var token = context.Request[tokenKey] ?? context.Request.Headers[tokenKey];
            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    var ticket = FormsAuthentication.Decrypt(token);
                    if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                    {
                        return ticket.Name.ToObject<UserIdentity>();
                    }
                }
                catch { }
            }
            return UserIdentity.Anonymouse;
        }

        public static string GetToken(UserIdentity identity)
        {
            var tokenValue = identity.ToJson();
            var ticket = new FormsAuthenticationTicket(tokenValue, true, int.MaxValue);
            return FormsAuthentication.Encrypt(ticket);
        }
    }
}