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
            var time = new AttendanceTime(GetAttendanceGroup(userId));
            if (!time.IsCheckTime(DateTime.Now))
            {
                throw new Exception("该时间段签到无效");
            }
            var model = new CheckInOut { UserId = userId };
            DB.CheckInOuts.Add(model);
            DB.SaveChanges();
            UpdateAttendance(model.UserId, time, model.CreateTime.Date);
        }

        public List<AttendanceGroup> GetAttendanceGroups()
        {
            return DB.AttendanceGroups.ToList();
        }

        public Dictionary<int,int> GetUserGroups()
        {
            return DB.Users.ToDictionary(e => e.ID, e => e.AttendanceGroupId);
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

        public AttendanceGroup GetAttendanceGroup(int userId)
        {
            var user = DB.Users.FirstOrDefault(e => e.ID == userId);
            if (user.AttendanceGroupId > 0)
            {
                return DB.AttendanceGroups.FirstOrDefault(e => e.ID == user.AttendanceGroupId);
            }
            return DB.AttendanceGroups.FirstOrDefault(e => e.Default);
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
            var form = Core.FormManager.GetModel(FormType.Leave);
            if (form == null || form.FLowId == 0)
            {
                throw new Exception("Form或流程未配置");
            }
            var info = new FormInfo
            {
                Title = $"[{((LeaveType)data.Category).GetDescription()}请假] {data.ScheduleBeginTime.ToString("yyyy-MM-dd HH:mm")} {data.ScheduleEndTime?.ToString("~ yyyy-MM-dd HH:mm")}",
                FormId = (int)FormType.Leave,
                PostUserId = data.UserId,
            };
            info.Form = form;
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

        public void SaveGroup(AttendanceGroup data)
        {
            if (data.ID > 0)
            {
                var entity = DB.AttendanceGroups.FirstOrDefault(e => e.ID == data.ID);
                if (entity.Default && !data.Default)
                {
                    data.Default = true;
                }
                else if (data.Default && !entity.Default)
                {
                    var defaultEntity = DB.AttendanceGroups.FirstOrDefault(e => e.Default);
                    defaultEntity.Default = false;
                }
                DB.Entry(entity).CurrentValues.SetValues(data);
            }
            else
            {
                if (data.Default)
                {
                    var defaultEntity = DB.AttendanceGroups.FirstOrDefault(e => e.Default);
                    if (defaultEntity != null)
                    {
                        defaultEntity.Default = false;
                    }
                }
                else
                {
                    if (DB.AttendanceGroups.Count() == 0)
                    {
                        data.Default = true;
                    }
                }
                DB.AttendanceGroups.Add(data);
            }
            DB.SaveChanges();
        }
    }
}
