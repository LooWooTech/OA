using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class FlowManager:ManagerBase
    {
        /// <summary>
        /// 作用：通过信息ID和信息类型获取Flow
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日13:52:43
        /// </summary>
        /// <param name="infoId"></param>
        /// <param name="infoType"></param>
        /// <returns></returns>
        public Flow Get(int infoId,int infoType)
        {
            using (var db = GetDbContext())
            {
                return db.Flows.FirstOrDefault(e => e.InfoID == infoId&&e.InfoType==infoType);
            }
        }
        /// <summary>
        /// 作用：添加
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日15:15:44
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public int Save(Flow flow)
        {
            using (var db = GetDbContext())
            {
                db.Flows.Add(flow);
                db.SaveChanges();
                return flow.ID;
            }
        }

        /// <summary>
        /// 作用：通过ID获取FLOW
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日14:02:55
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Flows.Find(id);
            }
        }
    }
}