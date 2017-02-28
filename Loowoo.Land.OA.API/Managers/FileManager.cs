using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    public class FileManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存文件记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月20日21:14:22
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public int Save(File file)
        {
            using (var db = GetDbContext())
            {
                db.Files.Add(file);
                db.SaveChanges();
                return file.ID;
            }
        }

        /// <summary>
        /// 作用：删除文件记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月22日17:31:38
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (var db = GetDbContext())
            {
                var entry = db.Files.Find(id);
                if (entry == null)
                {
                    return false;
                }
                db.Files.Remove(entry);
                db.SaveChanges();
                return true;
            }
        }
        /// <summary>
        /// 作用：关联文件与表单的关系
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日09:56:20
        /// </summary>
        /// <param name="fileIds"></param>
        /// <param name="infoId"></param>
        /// <param name="formId"></param>
        public void Relation(int[] fileIds,int infoId,int formId)
        {
            var sb = new StringBuilder();
            using (var db = GetDbContext())
            {
                foreach(var item in fileIds)
                {
                    var model = db.Files.Find(item);
                    if (model != null)
                    {
                        model.InfoID = infoId;
                        model.FormID = formId;
                        model.UpdateTime = DateTime.Now;
                        db.SaveChanges();
                    }
                }
            }
        }
        /// <summary>
        /// 作用：查询文件
        /// 作者：汪建龙
        /// 编写时间：2017年2月28日10:08:36
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<File> Search(FileParameter parameter)
        {
            using (var db = GetDbContext())
            {
                var query = db.Files.AsQueryable();
                if (parameter.InfoId.HasValue)
                {
                    query = query.Where(e => e.InfoID == parameter.InfoId.Value);
                }
                if (parameter.FormId.HasValue)
                {
                    query = query.Where(e => e.FormID == parameter.FormId.Value);
                }
                if (parameter.Type.HasValue)
                {
                    query = query.Where(e => e.Type == parameter.Type.Value);
                }
                query = query.OrderBy(e => e.UpdateTime).SetPage(parameter.Page);
                return query.ToList();
            }
        }
    }
}