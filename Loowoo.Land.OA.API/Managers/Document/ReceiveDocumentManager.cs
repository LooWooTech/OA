using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class ReceiveDocumentManager:ManagerBase
    {
        /// <summary>
        /// 作用：添加收文  调用函数钱，需要对收文对象进行参数验证  成功返回ID，失败返回0；
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日19:12:24
        /// </summary>
        /// <param name="recDoc"></param>
        /// <returns></returns>
        public int Save(ReceiveDocument recDoc)
        {
            using (var db = GetDbContext())
            {
                db.Receive_Documents.Add(recDoc);
                db.SaveChanges();
                return recDoc.ID;
            }
        }
        /// <summary>
        /// 作用：对收文进行编辑
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日19:19:51
        /// </summary>
        /// <param name="recDoc"></param>
        public void Edit(ReceiveDocument recDoc)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Receive_Documents.Find(recDoc.ID);
                if (entry != null)
                {
                    //entry.Number = recDoc.Number;
                    //entry.Title = recDoc.Title;
                    db.Entry(entry).CurrentValues.SetValues(recDoc);
                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 作用：查询收文
        /// 作者：汪建龙
        /// 编写时间：2017年2月13日19:28:37
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<ReceiveDocument> Search(ReceiveParameter parameter)
        {
            using (var db = GetDbContext())
            {
                var query = db.Receive_Documents.Where(e => e.Deleted == false).AsQueryable();
                query = query.OrderBy(e => e.ID).SetPage(parameter.Page);
                return query.ToList();
            }
        }

        /// <summary>
        /// 作用：对收文进行立卷  销毁登记
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日09:34:23
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filing"></param>
        public void Filing(int id,Filing filing)
        {
            using (var db = GetDbContext())
            {
                var doc = db.Receive_Documents.Find(id);
                if (doc != null)
                {
                    doc.Filing = filing;
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 作用：对收文进行删除标志
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日09:41:36
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var doc = db.Receive_Documents.Find(id);
                if (doc != null)
                {
                    doc.Deleted = true;
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 作用：通过ID获取收文
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日13:50:31
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ReceiveDocument Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Receive_Documents.Find(id);
            }
        }

    }
}