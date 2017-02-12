using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class DocumentManager:ManagerBase
    {
        /// <summary>
        /// 作用:查询公文
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日15:15:22
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<SendDocument> Search(DocumentParameter parameter)
        {
            using (var db = GetDbContext())
            {
                var query = db.Send_Documents.Where(e=>e.Deleted==false).AsQueryable();

                query = query.OrderBy(e => e.ID).SetPage(parameter.Page);
                return query.ToList();
            }
        }
        /// <summary>
        /// 作用：添加公文
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日15:37:34
        /// </summary>
        /// <param name="senddoc"></param>
        /// <returns></returns>
        public int Create(SendDocument senddoc)
        {
            using (var db = GetDbContext())
            {
                db.Send_Documents.Add(senddoc);
                db.SaveChanges();
                return senddoc.ID;
            }
        }
        /// <summary>
        /// 作用：删除公文
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日18:20:38
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var doc = db.Send_Documents.Find(id);
                if (doc == null)
                {
                    return false;
                }
                doc.Deleted = true;
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：通过公文ID 获取公文对象  
        /// 作者：汪建龙
        /// 编写时间：2017年2月11日18:31:45
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SendDocument Get(int id)
        {
            using (var db = GetDbContext())
            {
                var doc = db.Send_Documents.Find(id);
                return doc;
            }
        }
    }
}