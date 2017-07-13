using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Attendance
{
    public class AttendanceService
    {
        private Managers.ManagerCore Core = Managers.ManagerCore.Instance;

        public async System.Threading.Tasks.Task Start()
        {
            var time = Core.AttendanceManager.GetAttendanceTime();
            if (time.IsCheckTime(DateTime.Now))
            {
                var count = await Execute(time);
                if (count == 0)
                {
                    Thread.Sleep(100);
                }
            }
            else
            {
                //非打卡时间，每隔1小时，计算一次考勤情况
                Thread.Sleep(1000 * 60 * 60);
            }
        }

        public async Task<int> Execute(AttendanceTime time)
        {
            if (DateTime.Now >= time.AMBeginTime && DateTime.Now <= time.AMEndTime)
            {
                return await CheckLogs(time.AMBeginTime, time.PMEndTime);
            }
            else if (DateTime.Now >= time.PMBeginTime && DateTime.Now <= time.PMEndTime)
            {
                return await CheckLogs(time.PMBeginTime, time.PMEndTime);
            }
            else
            {
                return await CheckLogs(DateTime.Today, DateTime.Now);
            }
        }

        private async Task<int> CheckLogs(DateTime beginTime, DateTime endTime)
        {
            //根据当前时间 遍历打卡记录
            var logs = Core.AttendanceManager.GetLogs(new Parameters.CheckInOutParameter
            {
                BeginTime = beginTime,
                EndTime = endTime,
                FalseOrNullApiResult = true
            });

            foreach (var log in logs)
            {
                if (log.ApiResult == true) continue;
                var json = await InvokeApiAsync(log);
                var result = string.IsNullOrEmpty(json) ? default(bool) : false;
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                log.ApiResult = data.ContainsKey("success") && data["success"] == "true";
                Core.AttendanceManager.SaveApiResult(log);
            }
            return logs.Count();
        }

        private HttpClient _client = new HttpClient();
        public async Task<string> InvokeApiAsync(CheckInOut log)
        {
            return await _client.GetStringAsync($"http://dh.pingshikaohe.com/attendance/fin2.do?username={log.User.RealName}&tel={log.User.Mobile}");
        }
    }
}
