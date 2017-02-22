using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    /// <summary>
    /// 流程步骤管理
    /// </summary>
    public class FlowNodeManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存流程步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日16:24:08
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int Save(FlowNode node)
        {
            using (var db = GetDbContext())
            {
                db.Flow_Nodes.Add(node);
                db.SaveChanges();
                return node.ID;
            }
        }

        /// <summary>
        /// 作用：编辑流程步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日16:25:59
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Edit(FlowNode node)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Flow_Nodes.Find(node.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(node);
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：删除步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日16:29:25
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using(var db = GetDbContext())
            {
                var entry = db.Flow_Nodes.Find(id);
                if (entry == null)
                {
                    return false;
                }
                db.Flow_Nodes.Remove(entry);
                db.SaveChanges();
                return true;
            }
        }
    }
}