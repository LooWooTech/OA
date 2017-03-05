using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class CarManager:ManagerBase
    {
        /// <summary>
        /// 作用：
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:01:05
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public int Save(Car car)
        {
            using (var db = GetDbContext())
            {
                db.Cars.Add(car);
                db.SaveChanges();
                return car.ID;
            }
        }

        /// <summary>
        /// 作用：编辑车辆信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:04:31
        /// </summary>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool Edit(Car car)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Cars.Find(car.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(car);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：通过ID获取车辆信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:05:23
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Car Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Cars.Find(id);
            }
        }

        /// <summary>
        /// 作用：删除车辆
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:06:35
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Cars.Find(id);
                if (entry != null)
                {
                    db.Cars.Remove(entry);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 作用：获取所有车辆信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日10:14:01
        /// </summary>
        /// <returns></returns>
        public List<Car> Search()
        {
            using (var db = GetDbContext())
            {
                return db.Cars.ToList();
            }
        }
    }
}