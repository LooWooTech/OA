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
            var list = DB.UserMessages.Where(e => e.ToUserId == userId);
            foreach (var item in list)
            {
                item.HasRead = true;
            }
            DB.SaveChanges();
        }

        public IEnumerable<UserMail> GetList(MailParameter parameter)
        {
            var query = DB.UserMails.AsQueryable();
            if (parameter.MailId > 0)
            {
                query = query.Where(e => e.ID == parameter.MailId);
            }
            if (parameter.Deleted.HasValue)
            {
                query = query.Where(e => e.Deleted == parameter.Deleted.Value);
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
            if (parameter.Draft.HasValue)
            {
                query = query.Where(e => e.Mail.IsDraft == parameter.Draft.Value);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Send(Mail model)
        {
            model.IsDraft = false;
            DB.Mails.AddOrUpdate(model);
            DB.SaveChanges();


            var toUsers = model.ToUserIds?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(userId => new UserMail
            {
                MailId = model.ID,
                UserId = int.Parse(userId),
            });

            if (toUsers == null)
            {
                throw new Exception("没有选择收件人");
            }
            DB.UserMails.AddRange(toUsers);

            var ccUsers = model.CcUserIds?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(userId => new UserMail
            {
                MailId = model.ID,
                UserId = int.Parse(userId),
                CC = true
            });
            if (ccUsers != null)
            {
                DB.UserMails.AddRange(ccUsers);
            }
            DB.SaveChanges();
        }

        public void Save(Mail model)
        {
            DB.Mails.AddOrUpdate(model);
            DB.SaveChanges();

            SaveUserMail(new UserMail
            {
                MailId = model.ID,
                UserId = model.CreatorId,
                HasRead = true,
            });
        }

        public void SaveUserMail(UserMail model)
        {
            var entity = DB.UserMails.FirstOrDefault(e => e.MailId == model.ID && e.UserId == model.ID);
            if (entity != null)
            {
                DB.Entry(entity).CurrentValues.SetValues(model);
            }
            else
            {
                DB.UserMails.Add(model);
            }
            DB.SaveChanges();
        }
    }
}
