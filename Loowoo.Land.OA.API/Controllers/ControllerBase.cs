using Loowoo.Common;
using Loowoo.Land.OA.API.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class ControllerBase : ApiController
    {
        protected ManagerCore Core = ManagerCore.Instance;

        protected LogWriter LogWriter = LogWriter.Instance;
    }
}
