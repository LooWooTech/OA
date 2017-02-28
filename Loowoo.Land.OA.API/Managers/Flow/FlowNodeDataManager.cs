using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
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
            using (var db = GetDbContext())
            {
                var models = db.Flow_Node_Datas.Where(e => e.FlowDataId == flowDataId).ToList();
                return models;
            }
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
            using (var db = GetDbContext())
            {
                var models = db.Flow_Node_Datas.Where(e => e.UserId == userId).ToList();
                return models;
            }
        }
        /// <summary>
        /// 作用：用户某个流程记录的节点记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日13:43:35
        /// </summary>
        /// <param name="flowDataId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public FlowNodeData Get(int flowDataId,int userId)
        {
            using (var db = GetDbContext())
            {
                return db.Flow_Node_Datas.FirstOrDefault(e => e.FlowDataId == flowDataId && e.UserId == userId);
            }
        }
    }
}