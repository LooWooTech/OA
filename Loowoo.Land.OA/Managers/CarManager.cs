using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class CarManager : ManagerBase
    {
        public IEnumerable<Car> GetList()
        {
            return DB.Cars.Where(e => !e.Deleted);
        }

        public void Save(Car model)
        {
            model.Number = model.Number.ToUpper();
            if (DB.Cars.Any(e => e.Number == model.Number && (e.ID == 0 || e.ID != model.ID)))
            {
                throw new Exception("该车牌号已被使用");
            }

            DB.Cars.AddOrUpdate(model);
            DB.SaveChanges();
        }

        public Car Get(int id)
        {
            return DB.Cars.FirstOrDefault(e => e.ID == id);
        }

        public void Delete(int id)
        {
            var entity = Get(id);
            entity.Deleted = true;
            DB.SaveChanges();
        }
    }
}