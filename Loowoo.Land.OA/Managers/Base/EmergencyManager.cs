using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 缓急管理
    /// </summary>
    public class EmergencyManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存缓急
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日09:58:45
        /// </summary>
        /// <param name="emergency"></param>
        /// <returns></returns>
        public int Save(Emergency emergency)
        {
            db.Emergencys.Add(emergency);
            db.SaveChanges();
            return emergency.ID;
           
        }
        /// <summary>
        /// 作用：编辑缓急
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:00:20
        /// </summary>
        /// <param name="emergency"></param>
        /// <returns></returns>
        public bool Edit(Emergency emergency)
        {
            var model = db.Emergencys.Find(emergency.ID);
            if (model == null)
            {
                return false;
            }
            db.Entry(model).CurrentValues.SetValues(emergency);
            db.SaveChanges();
            return true;
          
        }
        /// <summary>
        /// 作用：删除缓急
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:04:24
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var model = db.Emergencys.Find(id);
            if (model == null || model.Deleted == true)
            {
                return false;
            }
            model.Deleted = true;
            db.SaveChanges();
            return true;
          
        }
        /// <summary>
        /// 作用：获取缓急信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:05:10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Emergency Get(int id)
        {
            return db.Emergencys.Find(id);
           
        }
        /// <summary>
        /// 作用：获取所有缓急情况列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:06:16
        /// </summary>
        /// <returns></returns>
        public List<Emergency> GetList()
        {
            return db.Emergencys.OrderBy(e => e.ID).ToList();
           
        }
        /// <summary>
        /// 作用：验证系统是否已存在缓急
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:07:49
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            var model = db.Emergencys.FirstOrDefault(e => e.Name.ToLower() == name.ToLower());
            return model != null;
           
        }
    }
}