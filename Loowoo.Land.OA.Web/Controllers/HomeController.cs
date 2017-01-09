using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Loowoo.Land.OA.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Calendar(DateTime date)
        {
            ViewBag.Date = date;
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
