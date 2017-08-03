using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class AttendanceManager : ManagerBase
    {
        /// <summary>
        /// 添加打卡记录
        /// </summary>
        public void AddCheckInOut(int userId)
        {
            var time = GetAttendanceTime();
            if (!time.IsCheckTime(DateTime.Now))
            {
                throw new Exception("该时间段签到无效");
            }
            var model = new CheckInOut { UserId = userId };
            DB.CheckInOuts.Add(model);
            DB.SaveChanges();
            UpdateAttendance(model.UserId, time, model.CreateTime.Date);
        }
        /// <summary>
        /// 更新考勤状态
        /// </summary>
        public void UpdateAttendance(int userId, AttendanceTime time, DateTime date)
        {
            var tomorrow = date.AddDays(1);
            var model = DB.Attendances.FirstOrDefault(e => e.UserId == userId && e.Date == date);
            if (model == null)
            {
                model = new Attendance
                {
                    UserId = userId,
                    Date = date,
                };
                DB.Attendances.Add(model);
            }
            var leaves = DB.FormInfoExtend1s.Where(e => e.Result.HasValue && e.ScheduleBeginTime <= date && e.ScheduleEndTime > date).ToList();
            var logs = DB.CheckInOuts.Where(e => e.UserId == userId && e.CreateTime > date && e.CreateTime < tomorrow).OrderBy(e => e.CreateTime).ToList();
            model.Check(logs, leaves, time);
            DB.SaveChanges();
        }

        public void SaveApiResult(CheckInOut log)
        {
            log.UpdateTime = DateTime.Now;
            var entity = DB.CheckInOuts.FirstOrDefault(e => e.ID == log.ID);
            DB.Entry(entity).CurrentValues.SetValues(log);
            DB.SaveChanges();
        }

        private DateTime ConvertConfigTimeToDateTime(string key, string defaultValue)
        {
            var timespan = Core.ConfigManager.GetValue(key, defaultValue).ToTimeSpan();
            return DateTime.Today.Add(timespan);
        }

        public AttendanceTime GetAttendanceTime()
        {
            return new AttendanceTime
            {
                AMBeginTime = ConvertConfigTimeToDateTime("AMBeginTime", "05:30"),
                AMEndTime = ConvertConfigTimeToDateTime("AMEndTime", "08:40"),
                PMBeginTime = ConvertConfigTimeToDateTime("PMBeginTime", "17:20"),
                PMEndTime = ConvertConfigTimeToDateTime("PMEndTime", "22:00"),
            };
        }

        public IEnumerable<CheckInOut> GetLogs(CheckInOutParameter parameter)
        {
            var query = DB.CheckInOuts.AsQueryable();
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.UserId);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.CreateTime >= parameter.BeginTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.CreateTime <= parameter.EndTime.Value);
            }
            if (parameter.HasChecked == false)
            {
                query = query.Where(e => e.ApiResult == null);
            }

            return query.OrderBy(e => e.ID);
        }

        public FormInfo Apply(FormInfoExtend1 data)
        {
            var info = new FormInfo
            {
                ExtendId = data.InfoId,
                Title = $"申请{((LeaveType)data.Category).GetDescription()} 请假时间：{data.ScheduleBeginTime.ToString("yyyy-MM-dd HH:mm")} {data.ScheduleEndTime?.ToString("~ yyyy-MM-dd HH:mm")}",
                FormId = (int)FormType.Leave,
                PostUserId = data.UserId,
            };
            info.Form = Core.FormManager.GetModel(FormType.Leave);

            Core.FormInfoManager.Save(info);
            Core.FormInfoExtend1Manager.Apply(info, data);
            return info;
        }

        public IEnumerable<Attendance> GetList(AttendanceParameter parameter)
        {
            var query = DB.Attendances.AsQueryable();
            if (parameter.UserId.HasValue)
            {
                query = query.Where(e => e.UserId == parameter.UserId.Value);
            }

            if (parameter.BeginDate.HasValue)
            {
                query = query.Where(e => e.Date >= parameter.BeginDate.Value);
            }
            if (parameter.EndDate.HasValue)
            {
                query = query.Where(e => e.Date <= parameter.EndDate.Value);
            }
            return query.OrderBy(e => e.Date);
        }
    }
}
