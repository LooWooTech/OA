using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class FeedController : ControllerBase
    {
        [HttpGet]
        public object List(int formId = 0, int userId = 0, DateTime? beginTime = null, int page = 1, int rows = 20)
        {
            var infoIds = Core.UserFormInfoManager.GetList(new UserFormInfoParameter
            {
                UserId = CurrentUser.ID
            }).Select(e => e.InfoId).ToArray();
            var parameter = new FeedParameter
            {
                Page = new PageParameter(page, rows),
                FormId = formId,
                InfoIds = infoIds
            };
            var list = Core.FeedManager.GetList(parameter);
            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.FormId,
                    FormType = e.Form == null ? null : e.Form.FormType.ToString(),
                    FormName = e.Form == null ? null : e.Form.Name,
                    e.FromUserId,
                    FromUser = e.FromUser == null ? null : e.FromUser.RealName,
                    ToUser = e.ToUser == null ? null : e.ToUser.RealName,
                    e.ToUserId,
                    e.InfoId,
                    FlowStep = e.Info == null ? null : e.Info.FlowStep,
                    Action = e.Action.GetDescription(),
                    e.Title,
                    e.Description,
                    e.CreateTime,
                }),
                Page = parameter.Page
            };
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var model = Core.FeedManager.GetModel(id);
            if (model.FromUserId != CurrentUser.ID)
            {
                throw new HttpException(403, "无法删除该动态");
            }
            Core.FeedManager.Delete(id);
        }
    }
}
