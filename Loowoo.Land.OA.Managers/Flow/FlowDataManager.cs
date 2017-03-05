using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    public class FlowDataManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存流程记录 调用钱需要验证FormId FlowId InfoId是否有效
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日17:20:52
        /// </summary>
        /// <param name="flowData"></param>
        /// <returns></returns>
        public int Save(FlowData flowData)
        {
            using (var db = GetDbContext())
            {
                db.Flow_Datas.Add(flowData);
                db.SaveChanges();
                return flowData.ID;
            }
        }
        /// <summary>
        /// 作用：验证系统中是否已存在记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日15:48:26
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public bool Exist(int infoId,int formId)
        {
            return Get(formId, infoId) != null;
        }
        /// <summary>
        /// 作用：通过InfoID和formId获取表单流程记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月24日16:08:55
        /// </summary>
        /// <param name="formId"></param>
        /// <param name="infoId"></param>
        /// <returns></returns>
        public FlowData Get(int formId,int infoId)
        {
            using (var db = GetDbContext())
            {
                var model = db.Flow_Datas.FirstOrDefault(e => e.InfoId == infoId && e.FormId == formId);
                return model;
            }
        }

        /// <summary>
        /// 作用：获取
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日10:10:46
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FlowData Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Flow_Datas.Find(id);
            }
        }

        /// <summary>
        /// 作用：获取ID列表的所有FlowData实体类
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日11:09:11
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<FlowData> GetList(int[] ids)
        {
            var list = new List<FlowData>();
            foreach(var id in ids)
            {
                var model = Get(id);
                if (model != null)
                {
                    list.Add(model);
                }
            }
            return list;
        }
    }
}