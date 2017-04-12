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
        public object List(int formId, int postUserId = 0, string searchKey = null, FlowStatus? status = null, int page = 1, int rows = 20)
        {
            var parameter = new UserFormInfoParameter
            {
                FormId = formId,
                Status = status,
                SearchKey = searchKey,
                Page = new PageParameter(page, rows),
                UserId = CurrentUser.ID,
                PostUserId = postUserId
            };

            var form = Core.FormManager.GetModel(formId);
            var flow = Core.FlowManager.Get(form.FLowId);

            var list = Core.UserFormInfoManager.GetList(parameter).Select(e => new UserFormInfoVM
            {
                ID = e.ID,
                CategoryId = e.Info.CategoryId,
                CreateTime = e.Info.CreateTime,
                FlowDataId = e.Info.FlowDataId,
                FlowStep = e.Info.FlowStep,
                Json = e.Info.Json,
                FormId = e.FormId,
                InfoId = e.Info.ID,
                PostUserId = e.Info.PostUserId,
                Status = e.Status,
                Title = e.Info.Title,
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

            var canEdit = model.PostUserId == CurrentUser.ID;
            var canSubmit = true;
            var canCancel = true;
            FlowNodeData currentUserflownodeData = null;
            if (model == null)
            {
                canSubmit = false;
                canCancel = false;
            }
            else if (model.FlowData == null)
            {
                canCancel = false;
                canSubmit = true;
                //todo 编辑权限
                canEdit = model.PostUserId == CurrentUser.ID;
            }
            else
            {
                //如果当前是在退回状态，则可以编辑，否则不能编辑
                canSubmit = model.FlowData.CanSubmit(CurrentUser.ID);
                canEdit = canSubmit;
                canCancel = model.FlowData.CanCancel(CurrentUser.ID);
                //获取当前用户的审批意见
                currentUserflownodeData = model.FlowData.Nodes.OrderByDescending(e => e.CreateTime).FirstOrDefault(e => e.UserId == CurrentUser.ID);
            }


            return new
            {
                model,
                canEdit,
                canSubmit,
                canCancel,
                flownodeData = currentUserflownodeData
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

            //编辑时不更新postUserId
            model.PostUserId = CurrentUser.ID;

            Core.FormInfoManager.Save(model);

            //只有在保存的时候，添加用户表
            if (isAdd)
            {
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = model.ID,
                    UserId = model.PostUserId,
                    Status = FlowStatus.Draft,
                    FormId = model.FormId,
                });
            }
            //更新动态
            Core.FeedManager.Save(new Feed
            {
                Action = isAdd ? FeedAction.Add : FeedAction.Edit,
                FormId = model.FormId,
                InfoId = model.ID,
                FromUserId = model.PostUserId,
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