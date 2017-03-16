using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 密级管理
    /// </summary>
    public class ConfidentialLevelManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存密级
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:10:48
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int Save(ConfidentialLevel level)
        {
            db.ConfidentialLevels.Add(level);
            db.SaveChanges();
            return level.ID;
          
        }

        /// <summary>
        /// 作用：编辑密级
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:12:23
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool Edit(ConfidentialLevel level)
        {
            var model = db.ConfidentialLevels.Find(level.ID);
            if (model == null)
            {
                return false;
            }
            db.Entry(model).CurrentValues.SetValues(level);
            db.SaveChanges();
            return true;
          
        }
        /// <summary>
        /// 作用：验证系统中是否已存在相同密级名称
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:13:34
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exist(string name)
        {
            var model = db.ConfidentialLevels.FirstOrDefault(e => e.Deleted == false && e.Name.ToLower() == name.ToLower());
            return model != null;
          
        }
        /// <summary>
        /// 作用：获取所有密级列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:15:12
        /// </summary>
        /// <returns></returns>
        public List<ConfidentialLevel> GetList()
        {
            return db.ConfidentialLevels.Where(e => e.Deleted == false).OrderBy(e => e.ID).ToList();
          
        }
        /// <summary>
        /// 作用：删除密级
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:20:16
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var model = db.ConfidentialLevels.Find(id);
            if (model == null)
            {
                return false;
            }
            model.Deleted = true;
            db.SaveChanges();
            return true;
          
        }
        /// <summary>
        /// 作用：获取密级信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日10:21:36
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ConfidentialLevel Get(int id)
        {
            return db.ConfidentialLevels.Find(id);
           
        }
    }
}