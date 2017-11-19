using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Loowoo.Land.OA.API.Controllers
{
    public class MailController : ControllerBase
    {
        [HttpGet]
        public object List(bool? star = false, bool? deleted = false, string searchKey = null, string action = "receive", int page = 1, int rows = 20)
        {
            var parameter = new MailParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows),
                Star = star,
                Deleted = deleted,
                SearchKey = searchKey,
                FromUserId = action == "send" ? CurrentUser.ID : 0,
                ToUserId = action == "receive" ? CurrentUser.ID : 0,
            };
            var list = Core.MailManager.GetList(parameter);

            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.Mail.Subject,
                    e.Mail.CreateTime,
                    e.HasRead,
                    e.FromUserId,
                    FromUser = e.FromUser == null ? null : e.FromUser.RealName,
                    e.ToUserId,
                    ToUser = e.ToUser == null ? null : e.ToUser.RealName,
                    e.Star,
                    e.Deleted,
                    e.CC,
                    e.MailId,
                }),
                Page = parameter.Page
            };
        }

        [HttpPost]
        public void Send(Mail data)
        {
            Core.MailManager.Send(data);
        }

        [HttpPost]
        public void Save(Mail data)
        {
            Core.MailManager.Save(data);
        }

        [HttpGet]
        public object Model(int id)
        {
            var model = Core.MailManager.GetModel(id);
            if (model == null)
            {
                throw new Exception("参数错误");
            }

            var userMails = Core.MailManager.GetList(new MailParameter { MailId = id });
            if (!userMails.Any(e => e.FromUserId == CurrentUser.ID || e.ToUserId == CurrentUser.ID))
            {
                throw new Exception("权限不足");
            }

            return new
            {
                model,
                ToUsers = userMails.Where(e => !e.CC).Select(e => new { e.ToUser.RealName }),
                CcUsers = userMails.Where(e => e.CC).Select(e => new { e.ToUser.RealName }),
                Attachments = Core.FileManager.GetList(new FileParameter { FormId = (int)FormType.Mail, InfoId = id })
            };
        }

        [HttpGet]
        public void Read(int id)
        {
            Core.MailManager.Read(id, CurrentUser.ID);
        }

        [HttpGet]
        public void ReadAll()
        {
            Core.MailManager.ReadAll(CurrentUser.ID);
        }

        [HttpGet]
        public void Star(int id)
        {
            Core.MailManager.UpdateStar(id, CurrentUser.ID, true);
        }

        [HttpGet]
        public void UnStar(int id)
        {
            Core.MailManager.UpdateStar(id, CurrentUser.ID, false);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            Core.MailManager.UpdateDelete(id, CurrentUser.ID, true);
        }

        [HttpGet]
        public void Recovery(int id)
        {
            Core.MailManager.UpdateDelete(id, CurrentUser.ID, false);
        }

    }
}