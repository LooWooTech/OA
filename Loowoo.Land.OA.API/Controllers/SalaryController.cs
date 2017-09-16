using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class SalaryController : ControllerBase
    {
        public void Import(string name)
        {
            //上传excel文档
            var file = new FileController().Upload(name);
            //Core.SalaryManager.Import(file.AbsolutelyPath);
        }
    }
}