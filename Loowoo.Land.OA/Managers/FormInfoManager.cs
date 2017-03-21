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
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.Title.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public void Save(FormInfo model)
        {
            var isAdd = model.ID == 0;
            FormInfo entity = null;
            if (model.ID > 0)
            {
                entity = DB.FormInfos.FirstOrDefault(e => e.ID == model.ID);
                entity.Title = model.Title;
                entity.Keywords = model.Keywords;
                entity.CategoryId = model.CategoryId;
                entity.FlowDataId = model.FlowDataId;
                if (model.Data != null)
                {
                    entity.Data = model.Data;
                }
            }
            else
            {
                entity = model;
                DB.FormInfos.Add(entity);
            }
            entity.UpdateTime = model.UpdateTime ?? DateTime.Now;
            DB.SaveChanges();
        }

        public bool HasDeleteRight(FormInfo info, User user)
        {
            var action = "Form.Delete." + info.FormId + ".";
            return info.PostUserId == user.ID && user.HasRight(action);
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
