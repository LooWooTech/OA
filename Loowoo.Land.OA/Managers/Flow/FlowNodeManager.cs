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
            DB.FlowNodes.Add(node);
            DB.SaveChanges();
            return node.ID;
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
            var entry = DB.FlowNodes.Find(node.ID);
            if (entry == null)
            {
                return false;
            }
            DB.Entry(entry).CurrentValues.SetValues(node);
            DB.SaveChanges();
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
            var entry = DB.FlowNodes.Find(id);
            if (entry == null)
            {
                return false;
            }
            DB.FlowNodes.Remove(entry);
            DB.SaveChanges();
            return true;
        
        }

        /// <summary>
        /// 作用：通过流程模板的所有节点
        /// 作者：汪建龙
        /// 编写时间：2017年2月25日13:59:28
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public IEnumerable<FlowNode> GetList(int flowId)
        {
            return DB.FlowNodes.Where(e => e.FlowId == flowId);
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
            if (id <= 0)
            {
                return null;
            }
            var model = DB.FlowNodes.Find(id);
            if (model != null)
            {
                model.Next = DB.FlowNodes.FirstOrDefault(e => e.PrevId == model.ID);
            }
            return model;
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
            return DB.FlowNodeDatas.Any(e => e.FlowNodeId == id);
        }

        /// <summary>
        /// 作用：获取上一个流程节点
        /// 作者：汪建龙
        /// 编写时间：2017年3月23日09:35:51
        /// </summary>
        /// <param name="currentflowNodeID"></param>
        /// <returns></returns>
        public FlowNode GetPre(int currentflowNodeID)
        {
            var currentflownode = Get(currentflowNodeID);
            if (currentflownode == null)
            {
                return null;
            }
    
            return currentflownode.Prev;
        }
        /// <summary>
        /// 作用：通过当前节点获取下一个节点
        /// 作者：汪建龙
        /// 编写时间：2017年3月23日09:38:41
        /// </summary>
        /// <param name="currentFlowNodeId"></param>
        /// <returns></returns>
        public FlowNode GetNext(int currentFlowNodeId)
        {
            var currentFlowNode = Get(currentFlowNodeId);
            if (currentFlowNode == null)
            {
                return null;
            }
           
            return currentFlowNode.Next;
        }
    }
}