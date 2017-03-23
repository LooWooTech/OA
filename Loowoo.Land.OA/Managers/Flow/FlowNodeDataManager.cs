using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class FlowNodeDataManager:ManagerBase
    {
        /// <summary>
        /// 作用：通过FLowDataID获取相关流程节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日09:52:58
        /// </summary>
        /// <param name="flowDataId"></param>
        /// <returns></returns>
        public List<FlowNodeData> GetList(int flowDataId)
        {
            if (flowDataId <= 0)
            {
                return null;
            }
            return DB.FlowNodeDatas.Where(e => e.FlowDataId == flowDataId).ToList();
        }

        /// <summary>
        /// 作用：获取发送给某一用户某一表单的流程节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日10:08:12
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<FlowNodeData> GetByUserID(int userId)
        {
            var models = DB.FlowNodeDatas.Where(e => e.UserId == userId).ToList();
            return models;
           
        }
        /// <summary>
        /// 作用：用户某个流程记录的节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日13:43:35
        /// </summary>
        /// <param name="flowDataId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public FlowNodeData Get(int flowDataId,int userId,int flowNodeId)
        {
            return DB.FlowNodeDatas.FirstOrDefault(e => e.FlowDataId == flowDataId && e.UserId == userId && e.FlowNodeId == flowNodeId);
        }
        /// <summary>
        /// 作用：获取某一用户某流程记录的节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年3月5日12:10:28
        /// </summary>
        /// <param name="flowDataId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public FlowNodeData Get(int flowDataId,int userId)
        {
            return DB.FlowNodeDatas.FirstOrDefault(e => e.FlowDataId == flowDataId && e.UserId == userId);
        }
        /// <summary>
        /// 作用：只是通过flowDataID和FlowNodeID 获取
        /// 作者：汪建龙
        /// 编写时间：2017年3月22日17:10:44
        /// </summary>
        /// <returns></returns>
        public FlowNodeData GetFlowNodeData(int flowdataID,int flownodeId)
        {
            return DB.FlowNodeDatas.FirstOrDefault(e => e.FlowDataId == flowdataID && e.FlowNodeId == flownodeId);
        }
        /// <summary>
        /// 作用：获取上一个流程节点记录（取最近时间的审核通过的）
        /// 作者：汪建龙
        /// 编写时间：2017年3月23日10:13:47
        /// </summary>
        /// <param name="flowDataId"></param>
        /// <param name="currentFlowNodeId">当前流程节点ID</param>
        /// <returns></returns>
        public FlowNodeData GetPreFlowNodeData(int flowDataId,int currentFlowNodeId)
        {
            var preflowNode = Core.FlowNodeManager.GetPre(currentFlowNodeId);
            if (preflowNode == null)
            {
                return null;
            }
            return DB.FlowNodeDatas.Where(e => e.FlowDataId == flowDataId && e.FlowNodeId == preflowNode.ID && e.Result == true).OrderByDescending(e => e.UpdateTime).FirstOrDefault();
        }
        /// <summary>
        /// 作用：保存流程节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日14:53:06
        /// </summary>
        /// <param name="nodeData"></param>
        /// <returns></returns>
        public int Save(FlowNodeData nodeData)
        {
            DB.FlowNodeDatas.Add(nodeData);
            DB.SaveChanges();
            return nodeData.ID;
           
        }
        /// <summary>
        /// 作用：编辑流程节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日14:54:59
        /// </summary>
        /// <param name="nodeData"></param>
        /// <returns></returns>
        public bool Edit(FlowNodeData nodeData)
        {
            var model = DB.FlowNodeDatas.Find(nodeData.ID);
            if (model == null)
            {
                return false;
            }
            DB.Entry(model).CurrentValues.SetValues(nodeData);
            DB.SaveChanges();
            return true;
        }

        /// <summary>
        /// 作用：获取流程节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日15:55:02
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FlowNodeData Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.FlowNodeDatas.Find(id);
            }
        }

      
    }
}