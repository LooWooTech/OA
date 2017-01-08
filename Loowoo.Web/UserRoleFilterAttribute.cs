using Loowoo.Security;
using Loowoo.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Loowoo.Web
{
    [AttributeUsage(AttributeTargets.All, Inherited = true)]
    public class UserRoleFilterAttribute : ActionFilterAttribute
    {
        private UserRole _role;

        public UserRoleFilterAttribute(UserRole role)
        {
            _role = role;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var identity = (UserIdentity)Thread.CurrentPrincipal.Identity;
            if (identity.Role < _role)
            {
                throw new HttpException(401, "权限不足");
            }
            base.OnResultExecuting(filterContext);
        }
    }
}