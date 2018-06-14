using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Loowoo.Land.OA.Parameters;
using Loowoo.Land.OA.Models;
using Loowoo.Common;
using System.Data.Entity.Migrations;

namespace Loowoo.Land.OA.Managers
{
    public class MailManager : ManagerBase
    {
        public Mail GetModel(int id)
        {
            return DB.Mails.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<Mail> GetSendMails(MailParameter parameter)
        {
            var query = DB.Mails.AsQueryable();
            if (parameter.PostUserId > 0)
            {
                query = query.Where(e => e.Info.PostUserId == parameter.PostUserId);
            }
            if (parameter.Trash.HasValue)
            {
                query = query.Where(e => e.Deleted == parameter.Trash.Value);
            }
            if (parameter.Draft.HasValue)
            {
                query = query.Where(e => e.IsDraft == parameter.Draft.Value);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public IEnumerable<UserMail> GetUserMails(FormInfoParameter parameter)
        {
            var query = Core.UserFormInfoManager.GetUserInfoList<UserMail>(parameter);
            query = query.Where(e => e.IsDraft == false);
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Send(int id)
        {
            var entity = DB.Mails.FirstOrDefault(e => e.ID == id);
            entity.IsDraft = false;
            DB.SaveChanges();
        }

        public void Save(Mail model)
        {
            if (model.ID > 0)
            {
                var info = DB.FormInfos.FirstOrDefault(e => e.ID == model.ID);
                info.Title = model.Subject;

                var mail = DB.Mails.FirstOrDefault(e => e.ID == model.ID);
                mail.Content = model.Content;
                mail.Subject = model.Subject;

                var olds = DB.UserFormInfos.Where(e => e.InfoId == model.ID);
                DB.UserFormInfos.RemoveRange(olds);

                DB.UserFormInfos.AddRange(model.Users);

                //DB.UserMails.AddRange(model.Users);
            }
            else
            {
                var info = new FormInfo
                {
                    FormId = (int)FormType.Mail,
                    PostUserId = model.CreatorId,
                    Title = model.Subject,
                };
                DB.FormInfos.Add(info);
                DB.SaveChanges();

                model.ID = info.ID;
                DB.Mails.Add(model);
                DB.SaveChanges();
            }
        }

        //public void SaveUserMail(UserMail model)
        //{
        //    var entity = DB.UserMails.FirstOrDefault(e => e.MailId == model.MailId && e.UserId == model.UserId);
        //    if (entity != null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        DB.UserMails.Add(model);
        //    }
        //    DB.SaveChanges();
        //}

        public bool HasRight(int mailId, int userId)
        {
            return Core.FormInfoManager.HasRight(mailId, userId) || Core.UserFormInfoManager.HasRight(mailId, userId);
        }

        public void Delete(int mailId)
        {
            var entity = DB.Mails.FirstOrDefault(e => e.ID == mailId);
            if (entity != null)
            {
                entity.Deleted = true;
                if (entity.IsDraft)
                {
                    entity.Info.Deleted = true;
                    var userInfos = DB.UserFormInfos.Where(e => e.InfoId == entity.ID);
                    DB.UserFormInfos.RemoveRange(userInfos);
                }
                DB.SaveChanges();
            }
        }
    }
}
