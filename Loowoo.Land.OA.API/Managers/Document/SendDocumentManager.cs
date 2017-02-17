using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class SendDocumentManager:ManagerBase
    {
        /// <summary>
        /// 作用：通过ID获取发文信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:00:00
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SendDocument Get(int id)
        {
            using (var db = GetDbContext())
            {
                return db.Send_Documents.Find(id);
            }
        }
        /// <summary>
        /// 作用：添加发文信息
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:09:04
        /// </summary>
        /// <param name="senddoc"></param>
        /// <returns></returns>
        public int Save(SendDocument senddoc)
        {
            using (var db = GetDbContext())
            {
                db.Send_Documents.Add(senddoc);
                db.SaveChanges();
                return senddoc.ID;
            }
        }
        /// <summary>
        /// 作用：对发文信息进行编辑修改
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:13:56
        /// </summary>
        /// <param name="senddoc"></param>
        public void Edit(SendDocument senddoc)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Send_Documents.Find(senddoc.ID);
                if (entry != null)
                {
                    db.Entry(entry).CurrentValues.SetValues(senddoc);
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 作用：发文查询
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:34:07
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<SendDocument> Search(SendParameter parameter)
        {
            using (var db = GetDbContext())
            {
                var query = db.Send_Documents.Where(e => e.Deleted == false).AsQueryable();
                query = query.OrderBy(e => e.ID).SetPage(parameter.Page);
                return query.ToList();
            }
        }
        /// <summary>
        /// 作用：对发文进行归档处理
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日14:43:12
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filing"></param>
        public void Filing(int id,Filing filing)
        {
            using (var db = GetDbContext())
            {
                var send = db.Send_Documents.Find(id);
                if (send != null)
                {
                    send.Filing = filing;
                    db.SaveChanges();
                }
            }
        }
        /// <summary>
        /// 作用：删除发文
        /// 作者：汪建龙
        /// 编写时间：2017年2月14日15:11:25
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var send = db.Send_Documents.Find(id);
                if (send != null)
                {
                    send.Deleted = true;
                    db.SaveChanges();
                }
            }
        }
    }
}