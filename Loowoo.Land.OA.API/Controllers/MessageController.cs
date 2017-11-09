using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public object List(bool? hasRead = false, int action = 0, int page = 1, int rows = 20)
        {
            var parameter = new MessageParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows),
                HasRead = hasRead,
                FromUserId = action == 1 ? CurrentUser.ID : 0,
                ToUserId = action == 0 ? CurrentUser.ID : 0,
            };
            var list = Core.MessageManager.GetList(parameter);

            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.Message.InfoId,
                    e.Message.Content,
                    e.Message.CreateTime,
                    e.FromUserId,
                    FromUser = e.FromUser == null ? null : e.FromUser.RealName,
                    e.ToUserId,
                    ToUser = e.ToUser == null ? null : e.ToUser.RealName,

                    FormId = e.Message.Info == null ? 0 : e.Message.Info.FormId,
                    Title = e.Message.Info == null ? null : e.Message.Info.Title,
                }),
                Page = parameter.Page
            };
        }

        [HttpGet]
        public void Read(int id)
        {
            Core.MessageManager.Read(id, CurrentUser.ID);
        }

        [HttpGet]
        public void ReadAll()
        {
            Core.MessageManager.ReadAll(CurrentUser.ID);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.MessageManager.Delete(id, CurrentUser.ID);
        }
    }
}