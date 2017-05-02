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

        public bool HasApply(int carId, int userId)
        {
            var car = Get(carId);

            var query = DB.FormInfos.Where(e => e.ExtendId == carId && e.PostUserId == userId);
            if (car.UpdateTime.HasValue)
            {
                var info = query.OrderByDescending(e => e.CreateTime).FirstOrDefault(e => e.CreateTime > car.UpdateTime);
                if (info != null)
                {
                    return info.FlowData == null || !info.FlowData.Completed;
                }
                return false;
            }
            return query.Count() > 0;
        }

        public void Save(Car model)
        {
            model.Number = model.Number.ToLower();
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