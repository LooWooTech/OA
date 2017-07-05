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
        public object Month(int year, int month)
        {
            var beginDate = new DateTime(year, month, 1);
            var endDate = beginDate.AddMonths(1);
            var parameter = new AttendanceParameter
            {
                BeginDate = beginDate,
                EndDate = endDate,
                UserId = CurrentUser.ID
            };
            var list = Core.AttendanceManager.GetList(parameter);

            var leaves = Core.FormInfoExtend1Manager.GetList(new Extend1Parameter
            {
                BeginTime = beginDate,
                EndTime = endDate,
                UserId = CurrentUser.ID,
                Result = true,
            });
            return new
            {
                list,
                logs = Core.AttendanceManager.GetLogs(parameter),
                leaves = leaves.Select(e => new
                {
                    e.Info.Title,
                    e.ScheduleBeginTime,
                    e.ScheduleEndTime,
                    e.Reason,
                    e.Result,
                    e.ID,
                    e.CreateTime,
                    e.Category,
                    e.UserId,
                }),
                total = new
                {
                    Normal = list.Count(e => e.AMResult == AttendanceResult.Normal || e.PMResult == AttendanceResult.Normal),
                    Late = list.Count(e => e.AMResult == AttendanceResult.Late || e.PMResult == AttendanceResult.Late),
                    Early = list.Count(e => e.AMResult == AttendanceResult.Early || e.PMResult == AttendanceResult.Early),
                    Absent = list.Count(e => e.AMResult == AttendanceResult.Absent || e.PMResult == AttendanceResult.Absent),
                    OfficialLeave = leaves.Count(e => e.Category == (int)LeaveType.Official),
                    PersonalLeave = leaves.Count(e => e.Category == (int)LeaveType.Personal)
                },
                time = Core.AttendanceManager.GetAttendanceTime()
            };
        }

        [HttpGet]
        public void CheckInOut()
        {
            Core.AttendanceManager.AddCheckInOut(CurrentUser.ID);
        }

        [HttpGet]
        public IHttpActionResult Log(DateTime? date = null, int page = 1, int rows = 20)
        {
            throw new NotImplementedException();
        }
    }
}
