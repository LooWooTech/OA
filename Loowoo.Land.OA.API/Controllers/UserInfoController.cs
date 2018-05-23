using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class UserInfoController : ControllerBase
    {
        [HttpGet]
        public object MyList(int formId, FlowStatus status = FlowStatus.Doing, int page = 1, int rows = 5)
        {
            var list = Core.UserFormInfoManager.GetUserInfoList(new FormInfoParameter
            {
                FormId = formId,
                UserId = Identity.ID,
                FlowStatus = status,
                Read = false,
                Page = new Loowoo.Common.PageParameter(page, rows),
            });
            return list.Select(e => new
            {
                e.ID,
                e.FormId,
                e.InfoId,
                e.Title,
                e.CreateTime,
                e.UpdateTime,
                e.Reminded,
                e.CC,
                Poster = e.Poster == null ? null : e.Poster.RealName,
            });
        }

        [HttpGet]
        public void Read(int id)
        {
            Core.UserFormInfoManager.Read(id, Identity.ID);
        }

        [HttpGet]
        public void ReadAll()
        {
            Core.UserFormInfoManager.ReadAll(Identity.ID);
        }

        [HttpGet]
        public void Star(int id)
        {
            Core.UserFormInfoManager.UpdateStar(id, Identity.ID, true);
        }

        [HttpGet]
        public void UnStar(int id)
        {
            Core.UserFormInfoManager.UpdateStar(id, Identity.ID, false);
        }

        [HttpDelete]
        public void Trash(int id)
        {
            Core.UserFormInfoManager.UpdateTrash(id, Identity.ID, true);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.UserFormInfoManager.Delete(id, Identity.ID);
        }

        [HttpGet]
        public void Recovery(int id)
        {
            Core.UserFormInfoManager.UpdateTrash(id, Identity.ID, false);
        }
    }
}