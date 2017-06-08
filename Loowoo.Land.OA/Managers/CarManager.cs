using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
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

        /// <summary>
        /// 获取申请记录
        /// </summary>
        public IEnumerable<CarApply> GetApplies(CarApplyParameter parameter)
        {
            var query = DB.CarApplies.AsQueryable();
            if (parameter.CarId > 0)
            {
                query = query.Where(e => e.CarId == parameter.CarId);
            }
            if (parameter.UserId > 0)
            {
                query = query.Where(e => e.UserId == parameter.UserId);
            }
            if (parameter.BeginTime.HasValue)
            {
                query = query.Where(e => e.CreateTime > parameter.BeginTime.Value);
            }
            if (parameter.Result.HasValue)
            {
                query = query.Where(e => e.Result == parameter.Result.Value);
            }
            return query.OrderByDescending(e => e.ID).SetPage(parameter.Page);
        }

        public CarApply GetCarApply(int id)
        {
            return DB.CarApplies.FirstOrDefault(e => e.ID == id);
        }
    }
}