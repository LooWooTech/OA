using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loowoo.Land.OA.Web.Controllers
{
    public class CarController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Apply(int carId = 0)
        {
            return View();
        }
    }
}