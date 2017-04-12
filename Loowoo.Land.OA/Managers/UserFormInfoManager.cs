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

        public void Save(UserFormInfo model)
        {
            if (model.ID > 0)
            {
                var entity = DB.UserFormInfos.FirstOrDefault(e => e.ID == model.ID);
                entity.Status = model.Status;
            }
            else
            {
                DB.UserFormInfos.Add(model);
            }
            DB.SaveChanges();
        }

        public void Save(int infoId, int formId, int userId, FlowStatus status)
        {
            var model = GetModel(infoId, formId, userId) ?? new UserFormInfo
            {
                FormId = formId,
                InfoId = infoId,
                Status = status,
                UserId = userId,
            };
            Core.UserFormInfoManager.Save(model);
        }
    }
}
