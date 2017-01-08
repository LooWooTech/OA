using Loowoo.Web.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Web
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
                var userIdentity = AuthorizeHelper.GetIdentity(new HttpContextWrapper(context.Context));
                context.Context.User = new UserPrincipal(userIdentity);
            });
        }


    }
}