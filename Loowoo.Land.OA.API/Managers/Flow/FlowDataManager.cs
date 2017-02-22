using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
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
    }
}