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
        /// <summary>
        /// 作用：获取考勤结果
        /// 作者：汪建龙
        /// 编写时间：
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult List(DateTime beginDate,DateTime endDate)
        {

            return Ok();
        }

        /// <summary>
        /// 作用：
        /// 作者：汪建龙
        /// 编写时间：
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Statistics(int year,int month)
        {
            return Ok();
        }


        /// <summary>
        /// 作用：打卡记录
        /// 作者：汪建龙
        /// 编写时间：
        /// </summary>
        /// <param name="date"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Log(DateTime date,int page,int rows)
        {

            return Ok();
        }
    }
}
