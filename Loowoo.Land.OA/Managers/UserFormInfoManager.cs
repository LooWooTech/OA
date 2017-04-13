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
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.Info.CreateTime >= parameter.BeginTime.Value);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.Info.CreateTime <= parameter.EndTime.Value);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public UserFormInfo GetModel(int infoId, int formId, int userId)
        {
            return DB.UserFormInfos.FirstOrDefault(e => e.InfoId == infoId && e.UserId == userId && e.FormId == formId);
        }

        public void Delete(UserFormInfo model)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == model.InfoId && e.UserId == model.UserId && e.FormId == model.FormId);
            DB.UserFormInfos.Remove(entity);
            DB.SaveChanges();
        }

        public void Save(UserFormInfo model)
        {
            var entity = DB.UserFormInfos.FirstOrDefault(e => e.InfoId == model.InfoId && e.UserId == model.UserId && e.FormId == model.FormId);
            if (entity != null)
            {
                entity.FlowNodeDataId = model.FlowNodeDataId;
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
