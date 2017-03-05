using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
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
            var entry = db.Flow_Nodes.Find(node.ID);
            if (entry == null)
            {
                return false;
            }
            db.Entry(entry).CurrentValues.SetValues(node);
            db.SaveChanges();
            return true;
          
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
            var entry = db.Flow_Nodes.Find(id);
            if (entry == null)
            {
                return false;
            }
            db.Flow_Nodes.Remove(entry);
            db.SaveChanges();
            return true;
        
        }

        /// <summary>
        /// 作用：通过流程模板的所有节点
        /// 作者：汪建龙
        /// 编写时间：2017年2月25日13:59:28
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public List<FlowNode> GetByFlowID(int flowId)
        {
            var list = db.Flow_Nodes.Where(e => e.FlowId == flowId).OrderBy(e => e.BackNodeID).ToList();
            return list;
        }

        /// <summary>
        /// 作用：获取流程节点
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日15:41:31
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FlowNode Get(int id)
        {
            return db.Flow_Nodes.Find(id);
        }
        /// <summary>
        /// 作用：获取下一个流程节点  id为当前FlowNodeID
        /// 作者：汪建龙
        /// 编写时间：2017年3月3日13:30:34
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FlowNode GetNext(int id)
        {
            return db.Flow_Nodes.FirstOrDefault(e => e.BackNodeID == id);
        }
        /// <summary>
        /// 作用：验证ID 流程节点是否使用
        /// 作者：汪建龙
        /// 编写时间：2017年3月3日17:15:10
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Used(int id)
        {
            return db.Flow_Nodes.Any(e => e.BackNodeID == id) || db.Flow_Node_Datas.Any(e => e.FlowNodeId == id);
        }
    }
}