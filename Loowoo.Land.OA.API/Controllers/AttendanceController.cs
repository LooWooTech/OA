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
        /// <summary>
        /// 作用：获取考勤结果
        /// 作者：汪建龙
        /// 编写时间：2017年3月31日20:17:13
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult List(DateTime beginDate,DateTime endDate)
        {
            var parameter = new AttendanceParameter
            {
                BeginDate = beginDate,
                EndDate = endDate,
                UserId=CurrentUser.ID
            };
            var list = Core.AttendanceManager.Search(parameter);

            return Ok(list.ToArray());
        }
        /// <summary>
        /// 作用：打卡记录
        /// 作者：汪建龙
        /// 编写时间：2017年3月31日20:17:17
        /// </summary>
        /// <param name="date"></param>
        /// <param name="page"></param>
        /// <param name="rows"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Log(DateTime? date=null, int page = 1, int rows = 20)
        {
            var parameter = new AttendanceParameter
            {
                BeginDate=date,
                EndDate=date,
                UserId=CurrentUser.ID,
                Page = new Loowoo.Common.PageParameter(page, rows)
            };
            var list = Core.AttendanceManager.Search(parameter);
            var table = new PagingResult<Attendance>
            {
                List = list,
                Page = parameter.Page
            };
            return Ok(table);
        }

        /// <summary>
        /// 作用：统计考勤
        /// 作者：汪建龙
        /// 编写时间：2017年3月31日20:17:22
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Statistics(int year,int month)
        {
            var beginTime = Convert.ToDateTime(string.Format("{0}-{1}-01", year, month));
            var endtime = beginTime.AddMonths(1);
            var parameter = new AttendanceParameter
            {
                BeginDate=beginTime,
                EndDate=endtime,
                UserId=CurrentUser.ID
            };
            var list = Core.AttendanceManager.Search(parameter);//查询考勤列表
            var form = Core.FormManager.GetModel(FormType.Leave);
            var forminfo = Core.FormInfoManager.GetList(new FormInfoParameter
            {
                FormId = form.ID,
                PostUserId = CurrentUser.ID,
                BeginTime = beginTime,
                EndTime = endtime
            });//获取请假列表
            var statistic = new AttendanceStatistic
            {
                Normal = list.Where(e => e.State == AttendanceState.Normal).Count(),
                Late = list.Where(e => e.State == AttendanceState.Late).Count(),
                Early = list.Where(e => e.State == AttendanceState.Early).Count(),
                Absent = list.Where(e => e.State == AttendanceState.Absent).Count(),
                OfficialLeave = 0,
                PersonLeave =forminfo.Count() 
            };
            return Ok(statistic);
        }


  
    }
}
