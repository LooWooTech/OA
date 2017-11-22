using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class MessageManager : ManagerBase
    {
        public int GetUnreadCount(int userId)
        {
            return DB.UserMessages.Count(e => e.UserId == userId && !e.HasRead);
        }

        public IEnumerable<UserMessage> GetList(MessageParameter parameter)
        {
            var query = DB.UserMessages.Where(e => !e.Deleted);
            if (parameter.FromUserId > 0)
            {
                query = query.Where(e => e.Message.CreatorId == parameter.FromUserId);
            }
            if (parameter.ToUserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.ToUserId);
            }
            if (parameter.HasRead.HasValue)
            {
                query = query.Where(e => e.HasRead == parameter.HasRead.Value);
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.Message.InfoId > 0 && e.Message.Info.FormId == parameter.FormId);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Add(Feed feed)
        {
            Add(new Message(feed), feed.FromUserId, feed.ToUserId);
        }

        public void Add(Message model, params int[] toUserIds)
        {
            DB.Messages.AddOrUpdate(model);
            DB.SaveChanges();
            DB.UserMessages.AddRange(toUserIds.Select(toUserId => new UserMessage
            {
                UserId = toUserId,
                MessageId = model.ID,
            }));
            DB.SaveChanges();
        }

        public Message GetModel(int id)
        {
            return DB.Messages.FirstOrDefault(e => e.ID == id);
        }

        public void ReadAll(int userId)
        {
            var list = DB.UserMessages.Where(e => e.UserId == userId);
            foreach (var item in list)
            {
                item.HasRead = true;
            }
            DB.SaveChanges();
        }

        private UserMessage GetUserMessage(int messageId, int toUserId)
        {
            return DB.UserMessages.FirstOrDefault(e => e.MessageId == messageId && e.UserId == toUserId);
        }

        public void Read(int msgId, int toUserId)
        {
            var entity = GetUserMessage(msgId, toUserId);
            if (entity != null)
            {
                entity.HasRead = true;
                DB.SaveChanges();
            }
        }

        public void Delete(int msgId, int toUserId)
        {
            var entity = GetUserMessage(msgId, toUserId);
            if (entity != null)
            {
                entity.Deleted = true;
                DB.SaveChanges();
            }
        }
    }
}