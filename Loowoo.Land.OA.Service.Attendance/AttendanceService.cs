using Loowoo.Common;
using Loowoo.Land.OA.Managers;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Service.Attendance
{
    public class AttendanceService
    {
        private bool _stop = false;
        private Thread _worker;
        private AttendanceManager AttendanceManager = new AttendanceManager();
        private List<AttendanceTime> _times;
        private List<AttendanceGroup> _groups;
        private Dictionary<int, AttendanceGroup> _userGroups;

        private DateTime _minBeginTime;
        private DateTime _maxEndTime;

        public void Start()
        {
            _groups = AttendanceManager.GetAttendanceGroups();
            _times = _groups.Select(e => new AttendanceTime(e)).ToList();
            var defaultGroup = _groups.FirstOrDefault(e => e.Default);
            _userGroups = AttendanceManager.GetUserGroups().ToDictionary(e => e.Key, e => e.Value == 0 ? defaultGroup : _groups.FirstOrDefault(g => g.ID == e.Value));
            _minBeginTime = _times.Min(e => e.AMBeginTime);
            _maxEndTime = _times.Min(e => e.PMBeginTime);
            _worker = new Thread(() =>
            {
                while (!_stop)
                {
                    try
                    {
                        Dowork();
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

        private void Dowork()
        {
            var count = CheckLogs();
            if (count == 0)
            {
                if (_times.Any(t => t.IsCheckTime(DateTime.Now, 10)))
                {
                    Thread.Sleep(500);
                }
                else
                {
                    Thread.Sleep(1000 * 60);
                }
            }
            else
            {
                Thread.Sleep(1);
            }
        }

        private int CheckLogs()
        {
            //根据当前时间 遍历打卡记录
            var logs = AttendanceManager.GetLogs(new Parameters.CheckInOutParameter
            {
                BeginTime = DateTime.Today,
                EndTime = DateTime.Now,
                HasChecked = false,
            });
            foreach (var log in logs)
            {
                if (log.ApiResult.HasValue) continue;
                var json = InvokeApi(log);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                log.ApiResult = data.ContainsKey("success") && data["success"] == "true" && (data["msg"].Contains("成功") || data["msg"].Contains("您已"));
                log.ApiContent = data.ToJson();
                AttendanceManager.SaveApiResult(log);
                LogWriter.Instance.WriteLog($"[{DateTime.Now}]\t打卡{(log.ApiResult.Value ? "成功" : "失败")}：{log.ToJson()}\r\n");
            }
            return logs.Count();
        }

        private string GetApiHost(int userId)
        {
            if (_userGroups.ContainsKey(userId))
            {
                return _userGroups[userId].API;
            }
            var group = _groups.FirstOrDefault(e => e.Default) ?? _groups.FirstOrDefault();

            return group?.API;
        }

        private HttpClient _client = new HttpClient();
        private string _apiUrlFormat = AppSettings.Get("ApiUrl");
        public string InvokeApi(CheckInOut log)
        {
            var host = GetApiHost(log.UserId);
            var url = _apiUrlFormat.Replace("{host}", host).Replace("{username}", log.User.RealName).Replace("{tel}", log.User.Mobile);
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                return client.DownloadString(url);
            }
        }
    }
}
