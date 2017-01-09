using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Loowoo.Land.OA.Web.Controllers
{
    public class CalendarController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            return View();
        }
    }
}
