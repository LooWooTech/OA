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
            if (DB.FormInfoExtend1s.Any(e => e.ExtendInfoId == id))
            {
                throw new Exception("车辆已被使用，无法删除");
            }
            var entity = Get(id);
            entity.Deleted = true;
            DB.SaveChanges();
        }

        public void Apply(FormInfoExtend1 data)
        {
            var model = Get(data.ExtendInfoId);
            var info = new FormInfo
            {
                Title = "申请用车：" + model.Name + "（" + model.Number + "）",
                FormId = (int)FormType.Car,
                PostUserId = data.UserId,
            };
            info.Form = Core.FormManager.GetModel(FormType.Car);

            Core.FormInfoManager.Save(info);
            Core.FormInfoExtend1Manager.Apply(info, data);
        }

        public void UpdateStatus(int carId, CarStatus status)
        {
            var model = Get(carId);
            model.Status = status;
            DB.SaveChanges();
        }
    }
}