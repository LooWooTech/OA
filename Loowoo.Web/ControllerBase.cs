using Loowoo.Common;
using Loowoo.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Loowoo.Web
{
    [Authorize]
    public class ControllerBase : Controller
    {
        protected UserIdentity CurrentUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Thread.CurrentPrincipal.Identity is UserIdentity)
            {
                CurrentUser = (UserIdentity)Thread.CurrentPrincipal.Identity;
            }
            else
            {
                CurrentUser = UserIdentity.Anonymouse;
            }
            ViewBag.CurrentUser = CurrentUser;
            base.OnActionExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            if (filterContext.HttpContext.Response.StatusCode == 200)
            {
                filterContext.HttpContext.Response.StatusCode = filterContext.Exception.GetHttpStatusCode();
            }
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
            var ex = filterContext.Exception.GetInnerException();
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                filterContext.Result = Json(new
                {
                    result = 0,
                    message = ex.Message,
                    stackTrace = ex.StackTrace
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ViewBag.Exception = ex;
                filterContext.Result = View("Error");
            }
        }
    }
}