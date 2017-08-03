using Loowoo.Common;
using Loowoo.Land.OA.API.Models;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FormInfoController : ControllerBase
    {
        [HttpGet]
        public object List(int formId, int postUserId = 0, string searchKey = null, bool? completed = null, FlowStatus? status = null, int page = 1, int rows = 20)
        {
            var parameter = new FormInfoParameter
            {
                FormId = formId,
                Status = status,
                Page = new PageParameter(page, rows),
                UserId = CurrentUser.ID,
                PostUserId = postUserId,
                Completed = completed
            };

            var form = Core.FormManager.GetModel(formId);
            var flow = Core.FlowManager.Get(form.FLowId);

            var list = Core.UserFormInfoManager.GetList(parameter).Select(e => new FormInfoViewModel
            {
                ID = e.ID,
                CategoryId = e.Info.CategoryId,
                CreateTime = e.Info.CreateTime,
                FlowDataId = e.Info.FlowDataId,
                InfoId = e.Info.ID,
                PostUserId = e.Info.PostUserId,
                Status = e.Status,
                UpdateTime = e.Info.UpdateTime,
                UserId = e.UserId,
            }).ToList();

            return new PagingResult
            {
                List = list,
                Page = parameter.Page
            };
        }

        [HttpGet]
        public object Model(int id)
        {
            var model = Core.FormInfoManager.GetModel(id);
            if (model == null)
            {
                return BadRequest("参数错误");
            }

            var canView = Core.FormInfoManager.CanView(model.FormId, model.ID, CurrentUser.ID);
            if (!canView)
            {
                return BadRequest("您没有权限查看该文档");
            }

            var canSubmitFlow = true;
            var canEdit = true;
            var canCancel = false;
            var canSubmitFreeFlow = false;
            var canCompleteFreeFlow = false;
            var canComplete = false;
            var canBack = false;

            FlowNodeData flowNodeData = null;
            FreeFlowNodeData freeFlowNodeData = null;
            if (model.FlowDataId > 0)
            {
                canSubmitFlow = Core.FlowDataManager.CanSubmit(model.FlowData, CurrentUser.ID);
                canEdit = canSubmitFlow;
                canCancel = model.FlowData.CanCancel(CurrentUser.ID);

                flowNodeData = model.FlowData.GetLastNodeData();
                canComplete = model.FlowData.CanComplete(flowNodeData);
                canEdit = flowNodeData.UserId == CurrentUser.ID && !flowNodeData.Result.HasValue;
                //当前步骤如果是流程的第一步，则不能退
                canBack = model.FlowData.CanBack();

                //如果该步骤开启了自由流程
                freeFlowNodeData = flowNodeData.GetLastFreeNodeData(CurrentUser.ID);


                canSubmitFreeFlow = !model.FlowData.Completed && flowNodeData.CanSubmitFreeFlow(CurrentUser.ID);

                var user = Core.UserManager.GetModel(CurrentUser.ID);
                canCompleteFreeFlow = !model.FlowData.Completed && flowNodeData.CanCompleteFreeFlow(user);
            }

            var userformInfo = Core.UserFormInfoManager.GetModel(model.ID, CurrentUser.ID);

            return new
            {
                model,
                canView,
                canEdit,
                canSubmit = canSubmitFlow || canSubmitFreeFlow,
                canSubmitFlow,
                canSubmitFreeFlow,
                canCompleteFreeFlow,
                canCancel,
                canComplete,
                canBack,
                status = userformInfo.Status,
                flowNodeData,
                freeFlowNodeData
            };
        }

        [HttpPost]
        public IHttpActionResult Save(FormInfo model)
        {
            if (model.FormId == 0)
            {
                return BadRequest("formId不能为0");
            }

            var isAdd = model.ID == 0;
            if (isAdd)
            {
                model.PostUserId = CurrentUser.ID;
            }

            Core.FormInfoManager.Save(model);

            //初始化流程数据
            if (model.FlowDataId == 0)
            {
                model.Form = Core.FormManager.GetModel(model.FormId);
                Core.FlowDataManager.CreateFlowData(model);
            }

            //更新动态
            Core.FeedManager.Save(new Feed
            {
                Action = isAdd ? UserAction.Create : UserAction.Update,
                InfoId = model.ID,
                FromUserId = CurrentUser.ID,
                Type = FeedType.Info,
                Title = model.Title,
            });

            return Ok(model);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var model = Core.FormInfoManager.GetModel(id);
            if (model != null)
            {
                if (Core.FormInfoManager.HasDeleteRight(model, CurrentUser))
                {
                    Core.FormInfoManager.Delete(id);
                    return Ok();
                }
                else
                {
                    return BadRequest("无法删除");
                }
            }
            return BadRequest("参数错误");
        }
    }
}