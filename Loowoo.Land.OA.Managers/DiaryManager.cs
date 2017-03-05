using Loowoo.Common;
using Loowoo.Land.OA.Models;
using Loowoo.Land.OA.Parameters;
using System.Collections.Generic;
using System.Linq;

namespace Loowoo.Land.OA.Managers
{
    public class DiaryManager:ManagerBase
    {
        /// <summary>
        /// 作用：保存日志记录
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日14:38:19
        /// </summary>
        /// <param name="diary"></param>
        /// <returns></returns>
        public int Save(Diary diary)
        {
            using (var db = GetDbContext())
            {
                db.Diarys.Add(diary);
                db.SaveChanges();
                return diary.ID;
            }
        }

        /// <summary>
        /// 作用：查询
        /// 作者：汪建龙
        /// 编写时间：2017年2月16日14:44:08
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public List<Diary> Search(DiaryParameter parameter)
        {
            using (var db = GetDbContext())
            {
                var query = db.Diarys.AsQueryable();
                if (parameter.UID.HasValue)
                {
                    query = query.Where(e => e.UID == parameter.UID.Value);
                }
                query = query.OrderByDescending(e => e.Time).SetPage(parameter.Page);
                return query.ToList();
            }
        }
    }
}