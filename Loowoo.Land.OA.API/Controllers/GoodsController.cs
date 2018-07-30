using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class GoodsController : ControllerBase
    {
        [HttpGet]
        public object List(int categoryId = 0, string searchKey = null, int page = 1, int rows = 10)
        {
            var parameter = new GoodsParameter
            {
                CategoryId = categoryId,
                SearchKey = searchKey,
                Page = new Loowoo.Common.PageParameter(page, rows)
            };
            var list = Core.GoodsManager.GetList(parameter);
            return new PagingResult
            {
                List = list,
                Page = parameter.Page
            };
        }

        [HttpGet]
        public object ApplyList(int goodsId = 0, int applyUserId = 0, int approvalUserId = 0, CheckStatus status = CheckStatus.All, int page = 1, int rows = 10)
        {
            var parameter = new GoodsApplyParameter
            {
                GoodsId = goodsId,
                ApplyUserId = applyUserId,
                ApprovalUserId = approvalUserId,
                Status = status,
                Page = new Loowoo.Common.PageParameter(page, rows)
            };

            var list = Core.GoodsManager.GetApplyList(parameter);

            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.GoodsId,
                    e.Number,
                    e.ApplyUserId,
                    ApplyUserName = e.ApplyUser.RealName,
                    ApprovalUserName = e.ApprovalUser.RealName,
                    e.ApprovalUserId,
                    e.Goods.Name,
                    e.Info.CreateTime,
                    e.Result,
                    e.Note,
                }),
                Page = parameter.Page
            };
        }

        [HttpPost]
        public void Save(Goods model)
        {
            Core.GoodsManager.Save(model);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.GoodsManager.Delete(id);
        }

        [HttpGet]
        public void Register(int goodsId, int number)
        {
            if (!CurrentUser.HasRight("Form.Goods.Edit"))
            {
                throw new Exception("没有物品管理权限");
            }
            var model = new GoodsRegister
            {
                GoodsId = goodsId,
                Number = number,
                UserId = Identity.ID
            };
            Core.GoodsManager.SaveRegister(model);
        }

        [HttpGet]
        public void Apply(int goodsId, int number, int approvalUserId, string note = null)
        {
            var model = new GoodsApply
            {
                GoodsId = goodsId,
                ApplyUserId = Identity.ID,
                ApprovalUserId = approvalUserId,
                Number = number,
                Note = note
            };
            var goods = Core.GoodsManager.GetModel(model.GoodsId);
            if (model.Number > goods.Number)
            {
                throw new Exception("申请数量太多");
            }
            var apply = Core.GoodsManager.GetLastApply(goods.ID, Identity.ID);
            if (apply != null && !apply.Result.HasValue && !apply.Info.Deleted)
            {
                throw new Exception("您已经申请过该物品，尚未通过审核，请耐心等待");
            }
            var info = new FormInfo
            {
                FormId = (int)FormType.Goods,
                Title = $"申请物品{goods.Name}x{model.Number}",
                PostUserId = Identity.ID,
            };
            Core.FormInfoManager.Save(info);

            var flowData = Core.FlowDataManager.CreateFlowData(info);
            Core.FlowDataManager.SubmitToUser(flowData, model.ApprovalUserId);

            model.ID = info.ID;
            goods.Number -= model.Number;
            Core.GoodsManager.SaveApply(model);
        }

        [HttpGet]
        public void Approval(int applyId, bool result = true, string note = null)
        {
            var apply = Core.GoodsManager.GetApply(applyId);
            if (apply.ApprovalUserId != Identity.ID)
            {
                throw new Exception("你不能审核此申请");
            }
            apply.Result = result;
            apply.Note = note;
            Core.GoodsManager.Approval(apply);
        }

        [HttpGet]
        public void CancelApply(int applyId)
        {
            var model = Core.GoodsManager.GetApply(applyId);
            if (model == null || model.Result.HasValue || model.ApplyUserId != Identity.ID)
            {
                throw new Exception("无法取消");
            }
            Core.GoodsManager.CancelApply(model);
        }
    }
}