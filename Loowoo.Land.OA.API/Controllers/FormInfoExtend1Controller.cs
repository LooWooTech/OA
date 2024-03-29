﻿using Loowoo.Common;
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
        public PagingResult List(int formId, int infoId = 0, int extendInfoId = 0, int userId = 0, int applyUserId = 0, int approvalUserId = 0, CheckStatus status = CheckStatus.All, int page = 1, int rows = 20)
        {
            var parameter = new Extend1Parameter
            {
                FormId = formId,
                ExtendInfoId = extendInfoId == 0 ? infoId : extendInfoId,
                UserId = userId,
                PostUserId = applyUserId,
                ApprovalUserId = approvalUserId,
                Page = new PageParameter(page, rows)
            };
            switch (status)
            {
                case CheckStatus.All:
                    break;
                case CheckStatus.Checked:
                    parameter.FlowStatus = new[] { FlowStatus.Completed, FlowStatus.Done };
                    break;
                case CheckStatus.Uncheck:
                    parameter.FlowStatus = new[] { FlowStatus.Doing, FlowStatus.Back };
                    break;
            }
            var list = Core.FormInfoExtend1Manager.GetList(parameter).GroupBy(e => e.InfoId).Select(g => g.FirstOrDefault());
            return new PagingResult
            {
                List = list.Select(e => new
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
                    e.FlowDataId,
                    ApprovalUser = e.ApprovalUserName,
                    ApplyUser = e.ApplyUserName,
                    e.FormId,
                    e.Title,
                    e.FlowStep,
                    e.AttachmentId,
                    Completed = e.FlowData == null ? false : e.FlowData.Completed,
                    e.FlowData,
                    e.FlowStatus,
                }),
                Page = parameter.Page,
            };
        }

        [HttpGet]
        public void UpdateApproval(int id)
        {
            var model = Core.FormInfoExtend1Manager.GetModel(id);
            var flowData = model.Info.FlowData;
            var lastFlowNodeData = flowData.GetLastNodeData();
            if (flowData.Completed)
            {
                model.Result = lastFlowNodeData.Result;
            }
            model.ApprovalUserId = lastFlowNodeData.UserId;
            model.UpdateTime = DateTime.Now;
            Core.FormInfoExtend1Manager.Save(model);
        }

        [HttpGet]
        public void Approval(int id, string content = null, bool result = true, int toUserId = 0)
        {
            var info = Core.FormInfoManager.GetModel(id);
            var currentNodeData = info.FlowData.GetLastNodeData();
            if (currentNodeData.UserId != Identity.ID)
            {
                currentNodeData = info.FlowData.GetChildNodeData(currentNodeData.ID);
            }
            if (currentNodeData == null)
            {
                throw new Exception("您没有参与此次流程审核");
            }
            currentNodeData.Result = result;
            currentNodeData.Content = content;

            Core.FlowNodeDataManager.Submit(currentNodeData);
            Core.UserFormInfoManager.Save(new UserFormInfo
            {
                InfoId = id,
                FlowStatus = FlowStatus.Done,
                UserId = Identity.ID
            });
            var model = Core.FormInfoExtend1Manager.GetModel(id);

            if (toUserId > 0)
            {
                Core.FlowNodeDataManager.CreateChildNodeData(currentNodeData, toUserId);
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = id,
                    FlowStatus = FlowStatus.Doing,
                    UserId = toUserId
                });

                var feed = new Feed
                {
                    Action = UserAction.Submit,
                    InfoId = id,
                    Title = info.Title,
                    FromUserId = Identity.ID,
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
                model.ApprovalUserId = Identity.ID;
                model.Result = result;
                model.UpdateTime = DateTime.Now;
                Core.FlowDataManager.Complete(info);

                var feed = new Feed
                {
                    Action = UserAction.Submit,
                    Type = FeedType.Info,
                    FromUserId = Identity.ID,
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
        public void Back(int id, DateTime? backTime = null)
        {
            var info = Core.FormInfoManager.GetModel(id);
            var apply = Core.FormInfoExtend1Manager.GetModel(id);
            if (apply == null)
            {
                throw new Exception("参数错误");
            }

            var infoTypeName = info.Form.FormType.GetDescription();

            if (apply.UserId == Identity.ID)
            {
                apply.RealEndTime = backTime ?? DateTime.Now;
                apply.UpdateTime = DateTime.Now;

                Core.FormInfoExtend1Manager.Save(apply);
                switch (info.Form.FormType)
                {
                    case FormType.Car:
                        Core.CarManager.UpdateStatus(apply.ExtendInfoId, CarStatus.Unused);
                        break;
                    case FormType.MeetingRoom:
                        Core.MeetingRoomManager.UpdateStatus(apply.ExtendInfoId, MeetingRoomStatus.Unused);
                        break;
                    case FormType.Seal:
                        Core.SealManager.UpdateStatus(apply.ExtendInfoId, SealStatus.Unused);
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