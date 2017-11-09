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
                logs = Core.AttendanceManager.GetLogs(new CheckInOutParameter
                {
                    BeginTime = beginDate,
                    EndTime = endDate,
                    UserId = CurrentUser.ID,
                }),
                holiday = Core.HolidayManager.GetList(new HolidayParameter
                {
                    BeginDate = beginDate,
                    EndDate = endDate
                }),
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
                time = new AttendanceTime(Core.AttendanceManager.GetAttendanceGroup(CurrentUser.ID))
            };
        }

        [HttpGet]
        public IHttpActionResult CheckInOut()
        {
            var log = Core.AttendanceManager.AddCheckInOut(CurrentUser.ID);
            try
            {
                var userGroup = Core.AttendanceManager.GetAttendanceGroup(CurrentUser.ID);

                var url = AppSettings.Get("AttendanceApiUrl").Replace("{host}", userGroup.API).Replace("{username}", CurrentUser.RealName).Replace("{tel}", CurrentUser.Mobile);
                using (var client = new WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var json = client.DownloadString(url);
                    var data = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                    log.ApiResult = data.ContainsKey("success") && data["success"] == "true" && (data["msg"].Contains("成功") || data["msg"].Contains("您已"));
                    log.ApiContent = data.ToJson();
                    Core.AttendanceManager.SaveApiResult(log);
                    if (log.ApiResult == false)
                    {
                        return Ok("打卡完成，同步失败："+ data["msg"]);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return Ok($"打卡完成，同步失败：" + ex.Message);
            }
        }

        [HttpGet]
        public IEnumerable<Models.UserViewModel> ApprovalUsers(int id)
        {
            var model = Core.FormInfoExtend1Manager.GetModel(id);
            var hours = (int)(model.ScheduleEndTime.Value - model.ScheduleBeginTime).TotalHours;
            if (hours > 24)
            {
                var list = Core.HolidayManager.GetList(new HolidayParameter
                {
                    BeginDate = model.ScheduleBeginTime,
                    EndDate = model.ScheduleEndTime
                });
                for (var day = model.ScheduleBeginTime.Date; day > model.ScheduleEndTime.Value.Date; day = day.AddDays(1))
                {
                    if (list.Any(e => e.BeginDate <= day && e.EndDate >= day))
                    {
                        hours -= 24;
                    }
                }
            }
            if (hours <= 24)
            {
                return null;
            }
            else if (hours <= 48)
            {
                //如果当前用户的流程的parentid>0，则表示为上级的上级在审批，不需要再往下级
                var info = Core.FormInfoManager.GetModel(id);
                var flowNodeData = info.FlowData.Nodes.OrderByDescending(e => e.ID).FirstOrDefault(e => e.UserId == CurrentUser.ID);
                if (flowNodeData.ParentId > 0)
                {
                    return null;
                }
            }
            return Core.UserManager.GetParentTitleUsers(CurrentUser).Select(e => new Models.UserViewModel(e));
        }

        [HttpPost]
        public void Apply([FromBody]FormInfoExtend1 data)
        {
            if (data.ApprovalUserId == 0)
            {
                throw new Exception("没有选择审核人");
            }
            data.UserId = CurrentUser.ID;
            var info = Core.AttendanceManager.Apply(data);
            var feed = new Feed
            {
                InfoId = info.ID,
                Action = UserAction.Apply,
                Title = info.Title,
                Type = FeedType.Flow,
                ToUserId = data.ApprovalUserId,
                FromUserId = CurrentUser.ID,
            };
            Core.FeedManager.Save(feed);
            Core.MessageManager.Add(feed);
        }

        [HttpGet]
        public void Approval(int id, bool result = true, int toUserId = 0)
        {
            var info = Core.FormInfoManager.GetModel(id);
            var currentNodeData = info.FlowData.GetLastNodeData();
            if (currentNodeData.UserId != CurrentUser.ID)
            {
                currentNodeData = info.FlowData.GetChildNodeData(currentNodeData.ID);
            }
            if (currentNodeData == null)
            {
                throw new Exception("您没有参与此次流程审核");
            }
            currentNodeData.Result = result;
            Core.FlowNodeDataManager.Submit(currentNodeData);
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = id,
                Status = FlowStatus.Done,
                UserId = CurrentUser.ID
            });
            var model = Core.FormInfoExtend1Manager.GetModel(id);

            if (toUserId > 0)
            {
                Core.FlowNodeDataManager.CreateChildNodeData(currentNodeData, toUserId);
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = id,
                    Status = FlowStatus.Doing,
                    UserId = toUserId
                });

                var feed = new Feed
                {
                    Action = UserAction.Submit,
                    InfoId = id,
                    Title = info.Title,
                    FromUserId = CurrentUser.ID,
                    ToUserId = toUserId,
                    Type = FeedType.Info,
                };
                Core.FeedManager.Save(feed);
                Core.MessageManager.Add(feed);

                model.ApprovalUserId = toUserId;
                model.UpdateTime = DateTime.Now;
            }
            else
            {
                model.ApprovalUserId = CurrentUser.ID;
                model.Result = result;
                model.UpdateTime = DateTime.Now;
                Core.FlowDataManager.Complete(info);

                var feed = new Feed
                {
                    Action = UserAction.Submit,
                    Type = FeedType.Info,
                    FromUserId = CurrentUser.ID,
                    ToUserId = model.UserId,
                    Title = "你申请的假期已审核通过",
                    Description = info.Title,
                    InfoId = info.ID,
                };
                Core.FeedManager.Save(feed);
                Core.MessageManager.Add(feed);
            }
            Core.FormInfoExtend1Manager.Save(model);
        }

        [HttpGet]
        public IEnumerable<AttendanceGroup> Groups()
        {
            return Core.AttendanceManager.GetAttendanceGroups();
        }

        [HttpPost]
        public void SaveGroup(AttendanceGroup data)
        {
            Core.AttendanceManager.SaveGroup(data);
        }
    }
}
