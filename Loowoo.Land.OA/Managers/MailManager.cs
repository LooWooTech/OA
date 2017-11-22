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

        public void UpdateStar(int id, int userId, bool isStar = true)
        {
            var entity = DB.UserMails.FirstOrDefault(e => e.ID == id && e.UserId == userId);
            if (entity != null)
            {
                entity.Star = isStar;
                DB.SaveChanges();
            }
        }

        public void UpdateDelete(int id, int userId, bool deleted = true)
        {
            var entity = DB.UserMails.FirstOrDefault(e => e.ID == id && e.UserId == userId);
            if (entity != null)
            {
                entity.Deleted = deleted;
                DB.SaveChanges();
            }
        }

        public void Read(int id, int userId)
        {
            var entity = DB.UserMails.FirstOrDefault(e => e.ID == id && e.UserId == userId);
            if (entity != null)
            {
                entity.HasRead = true;
                DB.SaveChanges();
            }
        }

        public void ReadAll(int userId)
        {
            var list = DB.UserMails.Where(e => e.UserId == userId);
            foreach (var item in list)
            {
                item.HasRead = true;
            }
            DB.SaveChanges();
        }

        public IEnumerable<Mail> GetMails(MailParameter parameter)
        {
            var query = DB.Mails.AsQueryable();
            if (parameter.FromUserId > 0)
            {
                query = query.Where(e => e.CreatorId == parameter.FromUserId);
            }
            if (parameter.Draft.HasValue)
            {
                query = query.Where(e => e.IsDraft == parameter.Draft.Value);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Subject.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public IEnumerable<UserMail> GetUserMails(MailParameter parameter)
        {
            var query = DB.UserMails.Where(e => e.Mail.IsDraft == false);
            if (parameter.Deleted.HasValue)
            {
                query = query.Where(e => e.Deleted == parameter.Deleted);
            }
            if (parameter.FromUserId > 0)
            {
                query = query.Where(e => e.Mail.CreatorId == parameter.FromUserId);
            }
            if (parameter.ToUserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.ToUserId);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Mail.Subject.Contains(parameter.SearchKey));
            }
            if (parameter.Star.HasValue)
            {
                query = query.Where(e => e.Star == parameter.Star.Value);
            }
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
                var entity = DB.Mails.FirstOrDefault(e => e.ID == model.ID);
                entity.Content = model.Content;
                entity.Subject = model.Subject;

                var olds = DB.UserMails.Where(e => e.MailId == entity.ID);
                DB.UserMails.RemoveRange(olds);

                DB.UserMails.AddRange(model.Users);
            }
            else
            {
                DB.Mails.Add(model);
            }
            DB.SaveChanges();
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

        public bool HasRight(int id, int userId)
        {
            return DB.Mails.Any(e => e.ID == id && e.CreatorId == userId) || DB.UserMails.Any(e => e.MailId == id && e.UserId == userId);
        }
    }
}
