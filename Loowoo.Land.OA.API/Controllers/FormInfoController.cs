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

            var list = Core.UserFormInfoManager.GetList(parameter);

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
            //TODO判断当前用户是否有权限打开？
            return model;
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