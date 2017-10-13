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
        public int[] GetUserInfoIds(FormInfoParameter parameter)
        {
            var query = GetList(parameter);

            if (parameter.UserId > 0)
            {
                return query.Select(e => e.InfoId).ToArray();
            }
            else
            {
                return query.GroupBy(e => e.InfoId).Select(g => g.Key).SetPage(parameter.Page).ToArray();
            }
        }

        public IEnumerable<UserFormInfo> GetList(FormInfoParameter parameter)
        {
            var query = DB.UserFormInfos.Where(e => !e.Info.Deleted);
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
            if (parameter.ExcludeStatus.HasValue)
            {
                query = query.Where(e => e.Status != parameter.ExcludeStatus.Value);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.Info.CreateTime >= parameter.BeginTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.Info.CreateTime <= parameter.EndTime.Value);
            }
            if (parameter.Completed.HasValue)
            {
                query = query.Where(e => e.Info.FlowData != null && e.Info.FlowData.Completed == parameter.Completed.Value);
            }
            if (parameter.UserId > 0)
            {
                return query.Where(e => e.UserId == parameter.UserId).OrderByDescending(e => e.ID).SetPage(parameter.Page);
            }
            else
            {
                return query.OrderByDescending(e => e.ID);
            }
        }

        public int[] GetUserIds(int infoId)
        {
            return DB.UserFormInfos.Where(e => e.InfoId == infoId && !e.Info.Deleted).Select(e => e.UserId).ToArray();
        }

        public UserFormInfo GetModel(int infoId, int userId)
        {
            return DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId && !e.Info.Deleted);
        }

        public void Delete(int infoId, int userId)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId);
            DB.UserFormInfos.Remove(entity);
            DB.SaveChanges();
        }


        public void Save(UserFormInfo model)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == model.InfoId && e.UserId == model.UserId);
            if (entity != null)
            {
                entity.Status = model.Status;
            }
            else
            {
                DB.UserFormInfos.Add(model);
            }
            DB.SaveChanges();
        }
    }
}
