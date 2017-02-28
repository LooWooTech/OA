using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
    /// <summary>
    /// 文件关联管理
    /// </summary>
    public class FileRelationManager:ManagerBase
    {
        ///// <summary>
        ///// 作用：增加文件关联信息列表
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月27日09:16:18
        ///// </summary>
        ///// <param name="list"></param>
        //public void AddRange(List<FileRelation> list)
        //{
        //    using (var db = GetDbContext())
        //    {
        //        db.File_Relations.AddRange(list);
        //        db.SaveChanges();
        //    }
        //}
        ///// <summary>
        ///// 作用：批量删除某一类型的某一条信息的文件列表
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月27日09:16:50
        ///// </summary>
        ///// <param name="infoId"></param>
        ///// <param name="formId"></param>
        //public void Remove(int infoId,int formId)
        //{
        //    using (var db = GetDbContext())
        //    {
        //        var models = db.File_Relations.Where(e => e.InfoID == infoId && e.FormID==formId).ToList();
        //        if (models.Count > 0)
        //        {
        //            db.File_Relations.RemoveRange(models);
        //            db.SaveChanges();
        //        }
        //    }
        //}
        ///// <summary>
        ///// 作用：验证文件是否发生改变
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月27日14:53:39
        ///// </summary>
        ///// <param name="fileIds"></param>
        ///// <param name="infoId"></param>
        ///// <param name="formId"></param>
        ///// <returns></returns>
        //public bool Change(int[] fileIds,int infoId,int formId)
        //{
        //    var olds = Get(infoId, formId).Select(e=>e.ID).ToList();
        //    if (fileIds == null)
        //    {
        //        if (olds.Count == 0)
        //        {
        //            return false;
        //        }

        //    }
        //    if (olds.Count != fileIds.Count())
        //    {
        //        return true;
        //    }
            
        //    foreach(var id in fileIds)
        //    {
        //        if (!olds.Contains(id))
        //        {
        //            return true;
        //        }   
        //    }
        //    return false;
        //}
        ///// <summary>
        ///// 作用：获取某一信息的文件ID
        ///// 作者：汪建龙
        ///// 编写时间：2017年2月27日14:55:47
        ///// </summary>
        ///// <param name="infoId"></param>
        ///// <param name="formId"></param>
        ///// <returns></returns>
        //public List<FileRelation> Get(int infoId,int formId)
        //{
        //    using (var db = GetDbContext())
        //    {
        //        var models = db.File_Relations.Where(e => e.InfoID == infoId && e.FormID==formId).ToList();
        //        return models;
        //    }
        //}
    }
}