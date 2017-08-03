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

        public bool CanView(int formId, int infoId, int userId)
        {
            return DB.UserFormInfos.Any(e => e.InfoId == infoId && e.UserId == userId);
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
    }
}
