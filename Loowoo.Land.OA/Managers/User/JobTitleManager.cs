using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.Managers
{
    public class JobTitleManager : ManagerBase
    {
        public IEnumerable<JobTitle> GetList()
        {
            return DB.JobTitles.ToList();
        }

        public void Save(JobTitle model)
        {
            DB.JobTitles.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public void Delete(int id)
        {
            if (DB.FlowNodes.Any(e => e.JobTitleIds.Contains(id)))
            {
                throw new Exception("无法删除");
            }
            var entity = DB.JobTitles.FirstOrDefault(e => e.ID == id);
            DB.JobTitles.Remove(entity);
            DB.SaveChanges();
        }

        public JobTitle GetParent(int id)
        {
            var model = DB.JobTitles.Find(id);
            return (model != null && model.ParentId > 0) ? DB.JobTitles.FirstOrDefault(e => model.ParentId == e.ID) : null;
        }

        public JobTitle GetSub(int id)
        {
            return DB.JobTitles.FirstOrDefault(e => e.ParentId == id);
        }
    }
}
