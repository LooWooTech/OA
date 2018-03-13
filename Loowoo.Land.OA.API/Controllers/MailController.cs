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
                Trash = false,
                Draft = false,
            };
            switch (type)
            {
                case "trash":
                    parameter.Trash = true;
                    parameter.UserId = Identity.ID;
                    break;
                case "draft":
                    parameter.Draft = true;
                    parameter.PostUserId = Identity.ID;
                    break;
                case "star":
                    parameter.Starred = true;
                    parameter.UserId = Identity.ID;
                    break;
                case "send":
                    parameter.PostUserId = Identity.ID;
                    break;
                default:
                    parameter.UserId = Identity.ID;
                    break;
            }
            //发件箱、草稿箱
            if (type == "send" || type == "draft")
            {
                return SendList(parameter);
            }
            else
            {
                //收件箱、回收站、星标邮件
                return Receivelist(parameter);
            }
        }

        /// <summary>
        /// 发件箱、草稿箱
        /// </summary>
        public object SendList(MailParameter parameter)
        {
            var list = Core.MailManager.GetSendMails(parameter);
            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    MailId = e.ID,
                    e.Subject,
                    e.CreateTime,
                    e.IsDraft,
                    e.ReplyId,
                    e.ForwardId,
                    e.HasAttachments,
                    ToUsers = e.Users.Where(u => !u.CC).Select(u => new { u.UserId, u.User.RealName }),
                }),
                Page = parameter.Page,
            };
        }

        /// <summary>
        /// 收件箱、回收站、星标邮件
        /// </summary>
        [HttpGet]
        public object Receivelist(MailParameter parameter)
        {
            var list = Core.MailManager.GetUserMails(parameter);

            return new PagingResult
            {
                List = list.Select(e => new
                {
                    e.ID,
                    e.Subject,
                    e.CreateTime,
                    e.Read,
                    FromUser = e.Poster == null ? null : e.Poster.RealName,
                    e.UserId,
                    e.Starred,
                    e.Trash,
                    e.CC,
                    MailId = e.InfoId,
                    e.ReplyId,
                    e.ForwardId,
                }),
                Page = parameter.Page
            };
        }

        [HttpPost]
        public void Send(Mail model)
        {
            Save(model);
            Core.MailManager.Send(model.ID);

            var feed = new Feed
            {
                FromUserId = Identity.ID,
                Action = UserAction.Create,
                Title = model.Subject,
                InfoId = model.ID,
            };
            var msg = new Message { InfoId = model.ID, Content = model.Subject, CreatorId = Identity.ID };
            var toUserIds = model.Users.Select(e => e.UserId).ToArray();
            Core.FeedManager.Save(feed, toUserIds);
            Core.MessageManager.Add(msg, toUserIds);
        }

        [HttpPost]
        public void Save(Mail model)
        {
            model.IsDraft = true;
            model.CreatorId = Identity.ID;
            //如果是转发，则拷贝附件的ID
            var isForward = model.ID == 0 && model.ForwardId > 0;
            Core.MailManager.Save(model);
            if (model.Attachments != null)
            {
                if (isForward)
                {
                    Core.FileManager.CopyFiles(model.Attachments.Select(e => e.ID).ToArray(), model.ID);
                }
                else
                {
                    Core.FileManager.Relation(model.Attachments.Select(e => e.ID).ToArray(), model.ID);
                }
            }
        }

        [HttpGet]
        public object Model(int id)
        {
            var model = Core.MailManager.GetModel(id);
            if (model == null)
            {
                throw new Exception("参数错误");
            }
            if (model.IsDraft && model.CreatorId != Identity.ID)
            {
                throw new Exception("没有权限查看他人草稿");
            }

            if (!Core.MailManager.HasRight(id, Identity.ID))
            {
                throw new Exception("权限不足");
            }
            return new
            {
                model,
                fromUser = new { model.Info.PostUser?.ID, model.Info.PostUser?.RealName },
                userMail = model.Users.FirstOrDefault(e => e.InfoId == model.ID && e.UserId == Identity.ID),
                toUsers = model.Users.Where(u => !u.CC).Select(u => new { ID = u.UserId, u.User.RealName }),
                ccUsers = model.Users.Where(u => u.CC).Select(u => new { ID = u.UserId, u.User.RealName }),
                attachments = Core.FileManager.GetList(new FileParameter { FormId = (int)FormType.Mail, InfoId = id })
            };
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var model = Core.MailManager.GetModel(id);
            if(model.Info.PostUserId != Identity.ID)
            {
                throw new Exception("无法删除别人创建的邮件");
            }
            Core.MailManager.Delete(id);
        }
    }
}