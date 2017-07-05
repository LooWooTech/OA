using Loowoo.Common;
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
    public class HolidayController : ControllerBase
    {

        [HttpPost]
        public void Save(Holiday model)
        {
            Core.HolidayManager.Save(model);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.HolidayManager.Delete(id);
        }

        [HttpGet]
        public object List(DateTime? beginDate = null, int page = 1, int rows = 20)
        {
            var parameter = new HolidayParameter
            {
                BeginDate = beginDate,
                Page = new PageParameter(page, rows)
            };
            var list = Core.HolidayManager.GetList(parameter);
            return new
            {
                list,
                page = parameter.Page
            };
        }

        [HttpGet]
        public void GenerateWeeks(int year)
        {
            Core.HolidayManager.GenerateWeek(year);
        }
    }
}
