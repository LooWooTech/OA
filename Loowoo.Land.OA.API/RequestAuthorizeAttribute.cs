using Loowoo.Land.OA.Models;
using Loowoo.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Security;

namespace Loowoo.Land.OA.API
{
    public class RequestAuthorizeAttribute:AuthorizeAttribute
    {
        public UserRole Role { get; set; }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var authorization = actionContext.Request.Headers.Authorization;
            if ((authorization != null) && authorization.Parameter != null)
            {
                var encryptTicket = authorization.Parameter;
                if (ValidateTicket(encryptTicket))
                {
                    base.IsAuthorized(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                bool isAnonymous = attributes.Any(e => e is AllowAnonymousAttribute);
                if (isAnonymous)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
        }

        private bool ValidateTicket(string encryptTicket)
        {
            var strTicket = FormsAuthentication.Decrypt(encryptTicket).UserData;
            var index = strTicket.IndexOf("&");
            var name = strTicket.Substring(0, index);
            var password = strTicket.Substring(index + 1);
            var current = HttpContext.Current.Session[name];
            if (current == null)
            {
                return false;
            }
            var user = current as User;
            if (user.Name.ToLower() != name.ToLower() || user.Password != password||user.Role<Role)
            {
                return false;
            }
            return true;
        }
    }
}