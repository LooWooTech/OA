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
        public List<FlowStep> Get(int flowId)
        {
            using (var db = GetDbContext())
            {
                return db.Flow_Steps.Where(e => e.FlowID == flowId).OrderBy(e=>e.Step).ToList();
            }
        }
    }
}