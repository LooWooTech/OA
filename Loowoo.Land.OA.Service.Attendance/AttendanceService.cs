using Loowoo.Common;
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
        private bool _stop = false;
        private int _recountTimes = 0;
        private Thread _worker;
        public void Start()
        {
            _worker = new Thread(async () =>
            {
                while (!_stop)
                {
                    try
                    {
                        await Dowork();
                    }
                    catch (Exception ex)
                    {
                        LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t{ex.Message}\r\n{ex.StackTrace}\r\n");
                    }
                }
            });
            _worker.Start();
        }

        public void Stop()
        {
            _stop = true;
            _worker.Abort();
            _worker = null;
        }

        private async System.Threading.Tasks.Task Dowork()
        {
            var time = Core.AttendanceManager.GetAttendanceTime();
            if (time.IsCheckTime(DateTime.Now))
            {
                _recountTimes = 0;
                var count = await Execute(time);
                if (count == 0)
                {
                    //如果是上班时间，则调用次数比较快
                    if (DateTime.Now < time.AMEndTime)
                    {
                        Thread.Sleep(100);
                    }
                    else
                    {
                        Thread.Sleep(1000 * 60);
                    }
                }
            }
            else
            {
                if (_recountTimes < 3)
                {
                    var count = await CheckLogs(DateTime.Today, DateTime.Now);
                    _recountTimes++;
                }
                //非打卡时间，每隔1小时，计算一次考勤情况
                var now = DateTime.Now;
                //如果还没到上午打卡时间
                var ts = new TimeSpan(0, 1, 0);
                if (now < time.AMBeginTime)
                {
                    ts = time.AMBeginTime - now;
                }
                else if (now < time.PMBeginTime)
                {
                    ts = time.PMBeginTime - now;
                }

                var sleepLong = 60;
                if (ts.TotalHours > 1)
                {
                    sleepLong = 60 * 60;
                }
                else if (ts.TotalMinutes > 1)
                {
                    sleepLong = 60;
                }
                else
                {
                    sleepLong = 1;
                }

                if (sleepLong > 60)
                {
                    LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t未到打卡时间，休息{sleepLong / 60}分钟\r\n");
                }
                Thread.Sleep(1000 * sleepLong);
            }
        }

        private async Task<int> Execute(AttendanceTime time)
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
                LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t签到成功：{log.ID}-{log.UserId}\r\n");
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
