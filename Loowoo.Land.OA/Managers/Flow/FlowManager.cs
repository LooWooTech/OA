using Loowoo.Caching;
using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 流程头信息管理
    /// </summary>
    public class FlowManager : ManagerBase
    {
        public int Add(Flow flow)
        {
            using (var db = GetDbContext())
            {
                db.Flows.Add(flow);
                db.SaveChanges();
                return flow.ID;
            }
        }

        public bool Update(Flow flow)
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

        public void Save(Flow model)
        {
            DB.Flows.AddOrUpdate(model);
            DB.SaveChanges();
        }

        private string _hashId = "flows";
        public Flow Get(int id)
        {
            return DB.Flows.Find(id);
            //return Cache.HGetOrSet(_hashId, id.ToString(), () =>
            //{
            //    var model = DB.Flows.FirstOrDefault(e => e.ID == id);
            //    model.Nodes = DB.FlowNodes.Where(e => e.FlowId == model.ID).ToList();
            //    foreach (var item in model.Nodes)
            //    {
            //        item.Prev = model.Nodes.FirstOrDefault(e => e.ID == item.PrevId);
            //        if(item.Prev!=null)
            //        {
            //            item.Prev.Next = item;
            //        }
            //    }
            //    return model;
            //});
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
            var model = DB.Flows.Find(id);
            if (model == null)
            {
                return false;
            }
            DB.Flows.Remove(model);
            DB.SaveChanges();
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
            return DB.Flows.OrderBy(e => e.ID);
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