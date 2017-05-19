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
    public class MissiveManager : ManagerBase
    {
        public IEnumerable<Missive> GetList(MissiveParameter parameter)
        {
            var query = DB.Missives.AsQueryable();
            if (parameter.Ids != null)
            {
                query = query.Where(e => parameter.Ids.Contains(e.ID));
            }
            if (parameter.FormId > 0)
            {
                query = query.Where(e => e.Info.FormId == parameter.FormId);
            }
            if (!string.IsNullOrEmpty(parameter.SearchKey))
            {
                query = query.Where(e => e.WJ_BT.Contains(parameter.SearchKey));
            }
            return query.OrderByDescending(e => e.ID);
        }

        public void Save(Missive data)
        {
            var entity = DB.Missives.FirstOrDefault(e => e.ID == data.ID);
            if (entity == null)
            {
                DB.Missives.Add(data);
            }
            else
            {
                DB.Entry(entity).CurrentValues.SetValues(data);
            }
            DB.SaveChanges();
        }

        public Missive GetModel(int id)
        {
            return DB.Missives.FirstOrDefault(e => e.ID == id);
        }
    }
}
