using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.Managers
{
    /// <summary>
    /// 流程管理
    /// </summary>
    public class StepManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存步骤
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public int Save(Step step)
        {
            using (var db = GetDbContext())
            {
                db.Steps.Add(step);
                db.SaveChanges();
                return step.ID;
            }
        }
        /// <summary>
        /// 作用：编辑步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日21:29:48
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public bool Edit(Step step)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Steps.Find(step.ID);
                if (entry == null)
                {
                    return false;
                }
                db.Entry(entry).CurrentValues.SetValues(step);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：删除步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日21:31:04
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Steps.Find(id);
                if (entry == null)
                {
                    return false;
                }
                db.Steps.Remove(entry);
                db.SaveChanges();
                return true;
            }
        }

        /// <summary>
        /// 作用：通过ID获取某一个步骤
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日21:31:55
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Step Get(int id)
        {
            using (var db = GetDbContext())
            {
                var entry=db.Steps.Find(id);
                if (entry != null)
                {
                    entry.StepUser = db.Step_Uer.Where(e => e.StepID == entry.ID).ToList();
                }
                return entry;
            }
        }
        /// <summary>
        /// 作用：通过infoID和serialNumber 获取流程
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日22:10:11
        /// </summary>
        /// <param name="infoTypdID"></param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public Step Get(int infoTypdID,int serialNumber)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Steps.FirstOrDefault(e => e.InfoType == infoTypdID && e.SerialNumber == serialNumber);
                if (entry != null)
                {
                    entry.StepUser = db.Step_Uer.Where(e => e.StepID == entry.ID).ToList();
                }
                return entry;
            }
        }
        /// <summary>
        /// 作用：单独获取某一类审核流程  返回从1到2
        /// 作者：汪建龙
        /// 编写时间：2017年2月21日21:41:55
        /// </summary>
        /// <param name="infoTypeID">类别ID</param>
        /// <returns></returns>
        public List<Step> GetList(int infoTypeID)
        {
            using (var db = GetDbContext())
            {
                var query = db.Steps.Where(e => e.InfoType == infoTypeID);
                foreach(var item in query)
                {
                    item.StepUser = db.Step_Uer.Where(e => e.StepID == item.ID).ToList();
                }
                return query.OrderBy(e=>e.SerialNumber).ToList();
            }
        }
    }
}