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
                query = query.Where(e => e.FormId == parameter.FormId);
            }
            if (parameter.InfoIds != null)
            {
                query = query.Where(e => parameter.InfoIds.Contains(e.InfoId));
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

            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);

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

        public Feed GetModel(int id)
        {
            return DB.Feeds.Find(id);
        }
    }
}
