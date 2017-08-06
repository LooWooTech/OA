using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FormInfoExtend1Controller : ControllerBase
    {
        [HttpGet]
        public PagingResult List(int infoId = 0, int userId = 0, CheckStatus status = CheckStatus.All, int page = 1, int rows = 20)
        {
            var parameter = new Extend1Parameter
            {
                InfoId = infoId,
                UserId = userId,
                Status = status,
                Page = new PageParameter(page, rows)
            };
            return new PagingResult
            {
                List = Core.FormInfoExtend1Manager.GetList(parameter).Select(e => new
                {
                    e.ID,
                    e.UserId,
                    e.InfoId,
                    e.CreateTime,
                    e.ScheduleBeginTime,
                    e.ScheduleEndTime,
                    e.RealEndTime,
                    e.Reason,
                    e.Result,
                    e.UpdateTime,
                    e.ApprovalUserId,
                    ApprovalUser = e.ApprovalUser.RealName,
                    ApplyUser = e.User.RealName,
                    Title = e.Info.Title,
                    FlowStep = e.Info.FlowStep,
                    Completed = e.Info.FlowData == null ? false : e.Info.FlowData.Completed,
                }),
                Page = parameter.Page,
            };
        }


        [HttpGet]
        public void Approval(int infoId)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            //如果流程审核完成
            if (info.FlowData.Completed)
            {
                var model = Core.FormInfoExtend1Manager.Get(infoId);
                model.Result = info.FlowData.GetLastNodeData().Result.Value;
                Core.FormInfoExtend1Manager.Save(model);
                switch (info.Form.FormType)
                {
                    case FormType.Car:
                        Core.CarManager.UpdateStatus(model.InfoId, CarStatus.Using);
                        break;
                    case FormType.MeetingRoom:
                        Core.MeetingRoomManager.UpdateStatus(model.InfoId, MeetingRoomStatus.Using);
                        break;
                    case FormType.Seal:
                        Core.SealManager.UpdateStatus(model.InfoId, SealStatus.Using);
                        break;
                }
            }
        }

        [HttpGet]
        public void Back(int infoId)
        {
            var info = Core.FormInfoManager.GetModel(infoId);
            var apply = Core.FormInfoExtend1Manager.Get(infoId);
            if (apply == null)
            {
                throw new Exception("参数错误");
            }

            var infoTypeName = info.Form.FormType.GetDescription();

            if (apply.UserId == CurrentUser.ID)
            {
                apply.RealEndTime = DateTime.Now;
                apply.UpdateTime = DateTime.Now;

                Core.FormInfoExtend1Manager.Save(apply);
                switch (info.Form.FormType)
                {
                    case FormType.Car:
                        Core.CarManager.UpdateStatus(apply.InfoId, CarStatus.Unused);
                        break;
                    case FormType.MeetingRoom:
                        Core.MeetingRoomManager.UpdateStatus(apply.InfoId, MeetingRoomStatus.Unused);
                        break;
                    case FormType.Seal:
                        Core.SealManager.UpdateStatus(apply.InfoId, SealStatus.Unused);
                        break;
                }
            }
            else
            {
                throw new Exception("你不能归还该" + infoTypeName);
            }
        }

    }
}