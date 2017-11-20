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
        public object List(string searchKey = null, string type = "receive", int page = 1, int rows = 20)
        {
            var parameter = new MailParameter
            {
                Page = new Loowoo.Common.PageParameter(page, rows),
                SearchKey = searchKey,
                Deleted = false,
                Draft = false,
            };
            switch (type)
            {
                case "trash":
                    parameter.Deleted = true;
                    parameter.ToUserId = CurrentUser.ID;
                    break;
                case "draft":
                    parameter.Draft = true;
                    parameter.FromUserId = CurrentUser.ID;
                    break;
                case "star":
                    parameter.Star = true;
                    parameter.ToUserId = CurrentUser.ID;
                    break;
                case "send":
                    parameter.FromUserId = CurrentUser.ID;
                    break;
                default:
                    parameter.ToUserId = CurrentUser.ID;
                    break;
            }

            var list = Core.MailManager.GetList(parameter);

            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.Mail.Subject,
                    e.Mail.CreateTime,
                    e.HasRead,
                    FromUser = e.Mail.Creator == null ? null : e.Mail.Creator.RealName,
                    e.UserId,
                    ToUser = e.User == null ? null : e.User.RealName,
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
            data.CreatorId = CurrentUser.ID;
            Core.MailManager.Send(data);
        }

        [HttpPost]
        public void Save(Mail data)
        {
            data.IsDraft = true;
            data.CreatorId = CurrentUser.ID;
            if (data.Attachments != null)
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
            if (model.IsDraft && model.CreatorId != CurrentUser.ID)
            {
                throw new Exception("没有权限查看他人草稿");
            }
            else
            {
                var userMails = Core.MailManager.GetList(new MailParameter { MailId = id });
                if (!userMails.Any(e => e.UserId == CurrentUser.ID))
                {
                    throw new Exception("权限不足");
                }
            }

            model.Attachments = Core.FileManager.GetList(new FileParameter { FormId = (int)FormType.Mail, InfoId = id }).ToList();
            return model;
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