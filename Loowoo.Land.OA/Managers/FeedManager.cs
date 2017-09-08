using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class FeedManager : ManagerBase
    {
        public IEnumerable<Feed> GetList(FeedParameter parameter)
        {
            var query = DB.Feeds.Where(e => !e.Deleted);
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.Info.FormId == parameter.FormId);
            }
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.FromUserId == parameter.UserId || e.ToUserId == parameter.UserId);
            }
            if (parameter.InfoIds != null)
            {
                query = query.Where(e => parameter.InfoIds.Contains(e.InfoId) || e.InfoId == 0);
            }
            if (parameter.FromUserId > 0)
            {
                query = query.Where(e => e.FromUserId == parameter.FromUserId);
            }
            if (parameter.ToUserId > 0)
            {
                query = query.Where(e => e.ToUserId == parameter.ToUserId);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.CreateTime > parameter.BeginTime.Value);
            }
            return query.OrderByDescending(e => e.ID);
        }

        public void Save(Feed model)
        {
            DB.Feeds.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            var model = DB.Feeds.Find(id);
            if (model != null)
            {
                model.Deleted = true;
                DB.SaveChanges();
            }
        }

        public void Delete(Feed template)
        {
            if (template.ID > 0)
            {
                DB.Feeds.Remove(template);
            }
            else
            {
                var query = DB.Feeds.AsQueryable();
                if (template.ToUserId > 0)
                {
                    query = query.Where(e => e.ToUserId == template.ToUserId);
                }
                if (template.FromUserId > 0)
                {
                    query = query.Where(e => e.FromUserId == template.FromUserId);
                }
                if (template.InfoId > 0)
                {
                    query = query.Where(e => e.InfoId == template.InfoId);
                }
                DB.Feeds.RemoveRange(query);
            }
            DB.SaveChanges();
        }

        public Feed GetModel(int id)
        {
            return DB.Feeds.Find(id);
        }

        public Feed GetModel(int fromUserId, int toUserId, int infoId)
        {
            return DB.Feeds.FirstOrDefault(e => e.FromUserId == fromUserId && e.ToUserId == toUserId && e.InfoId == infoId);
        }
    }
}
