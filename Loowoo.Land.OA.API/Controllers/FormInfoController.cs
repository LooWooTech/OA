using Loowoo.Common;
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
        public object List(int formId, string searchKey, FlowStatus? status, int page = 1, int rows = 20)
        {
            var parameter = new UserFormInfoParameter
            {
                FormId = formId,
                Status = status,
                CurrentUserId = CurrentUser.ID,
                SearchKey = searchKey,
                Page = new PageParameter(page, rows),
            };

            var list = Core.UserFormInfoManager.GetList(parameter);

            return new PagingResult<object>
            {
                List = list,
                Page = parameter.Page
            };
        }

        [HttpPost]
        public IHttpActionResult Save(FormInfo data)
        {
            if (data.FormId == 0)
            {
                return BadRequest("formId不能为0");
            }
            var context = (HttpContextWrapper)Request.Properties["MS_HttpContext"];
            data.Data = new FormInfoData(context.Request.Form);

            var isAdd = data.ID == 0;
            var form = Core.FormManager.GetModel(data.FormId);
            data.Form = form;
            data.UpdateFileds();
            data.PostUserId = CurrentUser.ID;
            Core.FormInfoManager.Save(data);

            //只有在保存的时候，添加用户表
            if (isAdd)
            {
                Core.UserFormInfoManager.Save(new UserFormInfo
                {
                    InfoId = data.ID,
                    UserId = data.PostUserId,
                });
            }
            //更新动态
            Core.FeedManager.Save(new Feed
            {
                Action = isAdd ? FeedAction.Add : FeedAction.Edit,
                FormId = data.FormId,
                InfoId = data.ID,
                FromUserId = data.PostUserId,
            });
            return Ok(data);
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