using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loowoo.Land.OA.Web.Controllers
{
    public class DocumentController : ControllerBase
    {
        public ActionResult Index()
        {
            return RedirectToAction("SendList", Request.RequestContext.RouteData.Values);
        }

        public ActionResult Edit(int id = 0)
        {
            return View();
        }

        public ActionResult SendList(int page = 1, int rows = 20)
        {
            return View();
        }

        public ActionResult ReceiveList(int page = 1, int rows = 20)
        {
            return View();
        }

        public ActionResult Detail(int id = 0)
        {
            return View();
        }
    }
}