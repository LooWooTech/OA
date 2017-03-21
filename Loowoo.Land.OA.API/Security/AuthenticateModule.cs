using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Security
{
    public class AuthenticateModule : IHttpModule
    {

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler((obj, ea) =>
            {
                var identity = AuthorizeHelper.GetIdentity(context.Context);
                context.Context.User = new UserPrincipal(identity);
            });
        }

    }
}