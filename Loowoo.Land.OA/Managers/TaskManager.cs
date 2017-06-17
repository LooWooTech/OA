using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class TaskManager : ManagerBase
    {
        public IEnumerable<Task> GetList(FormInfoParameter parameter)
        {
            var infos = Core.UserFormInfoManager.GetList(parameter);
            parameter.InfoIds = infos.Select(e => e.InfoId).ToArray();

            var query = DB.Tasks.AsQueryable();
            if (parameter.InfoIds != null)
            {
                query = query.Where(e => parameter.InfoIds.Contains(e.ID));
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.Info.FormId == parameter.FormId);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.MC.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID);
        }

        public void Save(Task data)
        {
            var entity = DB.Tasks.FirstOrDefault(e => e.ID == data.ID);
            if (entity == null)
            {
                DB.Tasks.Add(data);
            }
            else
            {
                DB.Entry(entity).CurrentValues.SetValues(data);
            }
            DB.SaveChanges();
        }

        public Task GetModel(int id)
        {
            return DB.Tasks.FirstOrDefault(e => e.ID == id);
        }

        public void Delete(int id)
        {
            
        }
    }
}