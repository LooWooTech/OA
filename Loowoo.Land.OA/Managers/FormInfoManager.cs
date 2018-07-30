using Loowoo.Common;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class FormInfoManager : ManagerBase
    {
        public FormInfo GetModel(int id)
        {
            if (id < 1) return null;
            return DB.FormInfos.FirstOrDefault(e => e.ID == id);
        }

        public IEnumerable<FormInfo> GetList(FormInfoParameter parameter)
        {
            var query = DB.FormInfos.Where(e => !e.Deleted);
            if (parameter.CategoryId > 0)
            {
                query = query.Where(e => e.CategoryId == parameter.CategoryId);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.CreateTime > parameter.BeginTime.Value);
            }
            if (parameter.Completed.HasValue)
            {
                query = query.Where(e => e.FlowData.Completed == parameter.Completed);
            }
            if (parameter.EndTime.HasValue)
            {
                query = query.Where(e => e.CreateTime < parameter.EndTime.Value);
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.FormId == parameter.FormId);
            }
            if (parameter.InfoIds != null && parameter.InfoIds.Length > 0)
            {
                query = query.Where(e => parameter.InfoIds.Contains(e.ID));
            }
            if (parameter.PostUserId > 0)
            {
                query = query.Where(e => e.PostUserId == parameter.PostUserId);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        //public bool HasApplied(int userId, DateTime? lastTime, int extendId = 0)
        //{
        //    var query = DB.FormInfos.Where(e => e.PostUserId == userId);
        //    if (extendId > 0)
        //    {
        //        query = query.Where(e => e.ExtendId == extendId);
        //    }
        //    if (lastTime.HasValue)
        //    {
        //        query = query.Where(e => e.CreateTime > lastTime.Value);
        //    }
        //    var info = query.OrderByDescending(e => e.CreateTime).FirstOrDefault();
        //    if (info != null)
        //    {
        //        return info.FlowData == null || !info.FlowData.Completed;
        //    }
        //    return false;
        //}

        public void Save(FormInfo model)
        {
            var isAdd = model.ID == 0;
            FormInfo entity = null;
            if (model.ID > 0)
            {
                entity = DB.FormInfos.FirstOrDefault(e => e.ID == model.ID);
                entity.CategoryId = model.CategoryId;
                entity.FlowDataId = model.FlowDataId;
            }
            else
            {
                entity = model;
                DB.FormInfos.Add(entity);
            }
            entity.UpdateTime = model.UpdateTime ?? DateTime.Now;
            DB.SaveChanges();
        }

        public bool CanView(FormInfo info, User user)
        {
            return user.HasRight(info.Form.FormType, UserRightType.View) || DB.UserFormInfos.Any(e => e.InfoId == info.ID && e.UserId == user.ID);
        }

        public bool HasDeleteRight(FormInfo info, User user)
        {
            return info.PostUserId == user.ID;
            //var action = "Form.Delete." + info.FormId + ".";
            //return info.PostUserId == user.ID && user.HasRight(action);
        }

        public void Delete(int id)
        {
            var entity = DB.FormInfos.FirstOrDefault(e => e.ID == id);

            if (entity != null)
            {
                entity.Deleted = true;
                DB.SaveChanges();
            }
        }

        public FormInfo GetModelByUid(string fromXxid)
        {
            return DB.FormInfos.FirstOrDefault(e => e.Uid == fromXxid);
        }

        public bool HasRight(int infoId, int userId)
        {
            return DB.FormInfos.Any(e => e.ID == infoId && e.PostUserId == userId);
        }
    }
}
