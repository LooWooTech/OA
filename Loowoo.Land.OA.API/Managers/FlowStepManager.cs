using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class FlowStepManager:ManagerBase
    {
        /// <summary>
        /// 作用：通过FlowID获取流程列表
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日19:10:04
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public List<FlowStep> GetByFlowID(int flowId)
        {
            using (var db = GetDbContext())
            {
                return db.Flow_Steps.Where(e => e.FlowID == flowId).OrderBy(e=>e.Step).ToList();
            }
        }
        /// <summary>
        /// 作用：添加审核流程  返回流程ID
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日17:17:20
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public int Save(FlowStep step)
        {
            using (var db = GetDbContext())
            {
                db.Flow_Steps.Add(step);
                db.SaveChanges();
                return step.ID;
            }
        }
        /// <summary>
        /// 作用：通过Flow信息ID获取最近一次的审批FlowStep 没有审核信息：NULL
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日17:19:43
        /// </summary>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public FlowStep GetLastStep(int flowId)
        {
            return GetByFlowID(flowId).OrderByDescending(e => e.Step).FirstOrDefault();
        }

        /// <summary>
        /// 作用：获取
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日17:33:42
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FlowStep Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Flow_Steps.Find(id);
            }
        }
        /// <summary>
        /// 作用：删除
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日18:41:24
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Flow_Steps.Find(id);
                if (entry != null)
                {
                    db.Flow_Steps.Remove(entry);
                    db.SaveChanges();
                }
            }
        }
    }
}