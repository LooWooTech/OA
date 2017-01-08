using Loowoo.Common;
using Loowoo.Land.OA;
using Loowoo.Web;
using Loowoo.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Loowoo.Web
{
    public class AuthorizeHelper
    {
        public static UserIdentity GetIdentity(HttpContextBase context)
        {
            var tokenKey = AppSettings.Get("CookieName");
            var token = context.Request.Headers[tokenKey] ?? context.Request[tokenKey];
            try
            {
                var ticket = FormsAuthentication.Decrypt(token);
                if (ticket != null && !string.IsNullOrEmpty(ticket.Name))
                {
                    var values = ticket.Name.Split('|');
                    if (values.Length >= 3)
                    {
                        var userId = int.Parse(values[0]);
                        var name = values[1];
                        var role = Enum.Parse(typeof(UserRole), values[2]);
                        return new UserIdentity
                        {
                            ID = userId,
                            Name = name,
                            Role = (UserRole)role,
                        };
                    }
                }
            }
            catch
            {
            }
            return UserIdentity.Anonymouse;
        }

        public static void Login(HttpContextBase context, User user)
        {
            var tokenKey = AppSettings.Get("CookieName");
            var tokenValue = user.ID + "|" + user.Name + "|" + user.Role;
            var ticket = new FormsAuthenticationTicket(tokenValue, false, int.MaxValue);
            var cookie = new HttpCookie(tokenKey, FormsAuthentication.Encrypt(ticket));
            cookie.Expires = DateTime.Now.AddDays(1);
            context.Response.SetCookie(cookie);
        }

        public static void Logout(HttpContextBase context)
        {
            var tokenKey = AppSettings.Get("CookieName");
            var cookie = context.Request.Cookies.Get(tokenKey);
            if (cookie == null) return;
            cookie.Expires = DateTime.Now.AddYears(-1);
            cookie.Values.Remove(tokenKey);
            context.Response.SetCookie(cookie);
        }
    }
}