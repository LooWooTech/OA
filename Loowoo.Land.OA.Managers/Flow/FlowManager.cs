using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 流程头信息管理
    /// </summary>
    public class FlowManager : ManagerBase
    {
        ///// <summary>
        ///// 作用：通过信息ID和信息类型获取Flow
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月14日13:52:43
        ///// </summary>
        ///// <param name="infoId"></param>
        ///// <param name="infoType"></param>
        ///// <returns></returns>
        //public Flow Get(int infoId,int infoType)
        //{
        //    using (var db = GetDbContext())
        //    {
        //        return db.Flows.FirstOrDefault(e => e.InfoID == infoId&&e.InfoType==infoType);
        //    }
        //}





        /// <summary>
        /// 作用：添加流程头信息
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
        /// 作用：编辑流程模板
        /// 作者：汪建龙
        /// 编写时间：2017年2月27日17:27:30
        /// </summary>
        /// <param name="flow"></param>
        /// <returns></returns>
        public bool Edit(Flow flow)
        {
            using (var db = GetDbContext())
            {
                var model = db.Flows.Find(flow.ID);
                if (model == null)
                {
                    return false;
                }
                db.Entry(model).CurrentValues.SetValues(flow);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：通过ID获取流程头信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月17日14:02:55
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flow Get(int id)
        {
            return db.Flows.Find(id);

        }
        /// <summary>
        /// 作用：删除流程模板
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日09:13:30
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var model = db.Flows.Find(id);
            if (model == null)
            {
                return false;
            }
            db.Flows.Remove(model);
            db.SaveChanges();
            return true;
        }
        /// <summary>
        /// 作用：获取所有流程模板
        /// 作者：汪建龙
        /// 编写时间：2017年3月3日17:21:00
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Flow> GetList()
        {
            return db.Flows.OrderBy(e => e.ID);
        }
        ///// <summary>
        ///// 作用：通过表单ID获取该表单的流程头信息
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月22日16:13:39
        ///// </summary>
        ///// <param name="formId"></param>
        ///// <returns></returns>
        //public Flow GetByFormId(int formId)
        //{
        //    using (var db = GetDbContext())
        //    {
        //        return db.Flows.FirstOrDefault(e => e.FormId == formId);
        //    }
        //}
    }
}