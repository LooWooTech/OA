using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.Services;
using Loowoo.Land.OA.Managers;
using Loowoo.Land.OA.API.Security;
using System.Threading;

namespace Loowoo.Land.OA.API.Controllers
{
    [Authorize]
    public class ControllerBase : ApiController
    {
        protected ManagerCore Core = ManagerCore.Instance;

        protected UserIdentity CurrentUser
        {
            get
            {
                return (UserIdentity)Thread.CurrentPrincipal.Identity;
            }
        }

        protected string TaskName { get; set; }
    }
}
