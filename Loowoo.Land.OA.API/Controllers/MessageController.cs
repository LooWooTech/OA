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
        public object List(bool? hasRead = false, int formId = 0, string action = "receive", int page = 1, int rows = 20)
        {
            var parameter = new MessageParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows),
                HasRead = hasRead,
                FromUserId = action == "send" ? Identity.ID : 0,
                ToUserId = action == "receive" ? Identity.ID : 0,
                FormId = formId,
            };
            var list = Core.MessageManager.GetList(parameter).ToList();

            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.Message.InfoId,
                    e.MessageId,
                    e.Message.Content,
                    e.Message.CreateTime,
                    e.HasRead,
                    e.Message.CreatorId,
                    FromUser = e.Message.Creator == null ? null : e.Message.Creator.RealName,
                    e.UserId,
                    ToUser = e.User == null ? null : e.User.RealName,

                    FormId = e.Message.Info == null ? 0 : e.Message.Info.FormId,
                    FormName = e.Message.Info.Form == null ? null : e.Message.Info.Form.Name,
                    Title = e.Message.Info == null ? null : e.Message.Info.Title,
                }),
                Page = parameter.Page
            };
        }

        [HttpGet]
        public void Read(int id)
        {
            Core.MessageManager.Read(id, Identity.ID);
        }

        [HttpGet]
        public void ReadAll()
        {
            Core.MessageManager.ReadAll(Identity.ID);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.MessageManager.Delete(id, Identity.ID);
        }
    }
}