using Loowoo.Land.OA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loowoo.Land.OA.API.Managers
{
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
    }
}