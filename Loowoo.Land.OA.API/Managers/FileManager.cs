using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}