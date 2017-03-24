using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class UserFormInfoManager : ManagerBase
    {
        public IQueryable<UserFormInfo> GetList(UserFormInfoParameter parameter)
        {
            var query = DB.UserFormInfos.Where(e => !e.Info.Deleted);
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.UserId);
            }
            if (parameter.PostUserId > 0)
            {
                query = query.Where(e => e.Info.PostUserId == parameter.PostUserId);
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.Info.FormId == parameter.FormId);
            }
            if (!string.IsNullOrWhiteSpace(parameter.SearchKey))
            {
                query = query.Where(e => e.Info.Title.Contains(parameter.SearchKey));
            }
            if (parameter.CategoryId > 0)
            {
                query = query.Where(e => e.Info.CategoryId == parameter.CategoryId);
            }
            if (parameter.Status.HasValue)
            {
                query = query.Where(e => e.Status == parameter.Status.Value);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public UserFormInfo GetModel(int infoId, int userId)
        {
            return DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId);
        }

        public void Save(UserFormInfo model)
        {
            if (model.ID > 0)
            {
                var entity = DB.UserFormInfos.FirstOrDefault(e => e.ID == model.ID);
                DB.Entry(entity).CurrentValues.SetValues(model);
            }
            else
            {
                DB.UserFormInfos.Add(model);
            }
            DB.SaveChanges();
        }
    }
}
