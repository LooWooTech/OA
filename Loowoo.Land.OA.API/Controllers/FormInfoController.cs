using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FormInfoController : ControllerBase
    {
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
        public void Save(FormInfo model)
        {
            var isAdd = model.ID == 0;
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
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.FormInfoManager.Delete(id, e =>
            {
                var canEdit =
                if (e.PostUserId == id || CurrentUser.Role == UserRole.Administrator)
                {
                    
                }
            });
        }
    }
}