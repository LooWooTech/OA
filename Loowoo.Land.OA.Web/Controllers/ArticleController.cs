using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loowoo.Land.OA.Web.Controllers
{
    public class ArticleController : ControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult Edit(int id = 0)
        {
            return View();
        }

        public void Save(Article model)
        {

        }
    }
}