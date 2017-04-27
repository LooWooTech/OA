using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class AttendanceController : ControllerBase
    {
        [HttpGet]
        public IHttpActionResult List(int year, int month)
        {
            var parameter = new AttendanceParameter
            {
                BeginDate = new DateTime(year, month, 1),
                EndDate = new DateTime(year, month, 1).AddMonths(1),
                UserId = CurrentUser.ID
            };
            var list = Core.AttendanceManager.GetList(parameter);

            return Ok(list.ToArray());
        }

        [HttpGet]
        public IHttpActionResult Log(DateTime? date = null, int page = 1, int rows = 20)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IHttpActionResult Statistics(int year, int month)
        {
            throw new NotImplementedException();

        }
    }
}
